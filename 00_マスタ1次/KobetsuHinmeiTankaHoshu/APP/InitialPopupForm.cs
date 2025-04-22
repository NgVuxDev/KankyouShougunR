using System.Collections.Generic;
using System.Windows.Forms;
using KobetsuHinmeiTankaHoshu.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Logic;
using r_framework.Utility;
using System;

namespace KobetsuHinmeiTankaHoshu.APP
{
    public partial class InitialPopupForm : SuperPopupForm
    {
        public InitialPopupFormLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        public string TorihikisakiCd = string.Empty;
        public string TorihikisakiName = string.Empty;
        public string GyoushaCd = string.Empty;
        public string GyoushaName = string.Empty;
        public string GenbaCd = string.Empty;
        public string GenbaName = string.Empty;
        public string DateFrom = string.Empty;
        public string DateTo = string.Empty;
        public List<string> OutListSysId = null;
        public string beforGyoushaCd = string.Empty;
        public string beforGenbaCd = string.Empty;
        public short DenpyouKbnCd = 0;
        public bool IsTekiyou = false;
        /// <summary>
        /// 
        /// </summary>
        public InitialPopupForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            logic = new InitialPopupFormLogic(this);
            this.logic.WindowInit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void Reflection(object sender, System.EventArgs e)
        {
            if (!this.logic.ElementDecision())
            {
                this.GyoushaCd = this.GYOUSHA_CD.Text;
                this.GyoushaName = this.GYOUSHA_NAME_RYAKU.Text;
                this.GenbaCd = this.GENBA_CD.Text;
                this.GenbaName = this.GENBA_NAME_RYAKU.Text;
                this.TorihikisakiCd = this.TORIHIKISAKI_CD.Text;
                this.TorihikisakiName = this.TORIHIKISAKI_NAME_RYAKU.Text;
                if (this.YUUKOU_BEGIN.Text != string.Empty)
                {
                    this.DateFrom = this.YUUKOU_BEGIN.Value.ToString();
                }
                if (this.YUUKOU_END.Text != string.Empty)
                {
                    this.DateTo = this.YUUKOU_END.Value.ToString();
                }
                this.IsTekiyou = this.TEKIYOU_KBN.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FormClose(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    ControlUtility.ClickButton(this, "bt_func8");
                    break;
                case Keys.F12:
                    ControlUtility.ClickButton(this, "bt_func12");
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            if (beforGyoushaCd != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
            this.logic.SearchTorihikisakiByGyousha();
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupBeforeGyoushaCode()
        {
            this.beforGyoushaCd = this.GYOUSHA_CD.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupAfterGenbaCode()
        {
            if (!beforGyoushaCd.Equals(GYOUSHA_CD.Text) || !beforGyoushaCd.Equals(GENBA_CD.Text))
            {
                this.logic.SearchGenbaName(null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupBeforeGenbaCode()
        {
            this.beforGyoushaCd = this.GYOUSHA_CD.Text;
            this.beforGenbaCd = this.GENBA_CD.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }
            if (this.beforGyoushaCd != this.GYOUSHA_CD.Text)
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;

            }
            this.logic.SearchGyoushaName(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }
            if (this.logic.ErrorCheckGenba())
            {
                this.logic.SearchGenbaName(e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.SearchTorihikisakiName(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEKIYOU_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TEKIYOU_KBN.Checked)
            {
                this.YUUKOU_BEGIN.Text = string.Empty;
                this.YUUKOU_BEGIN.ReadOnly = true;
                this.YUUKOU_END.Text = string.Empty;
                this.YUUKOU_END.ReadOnly = true;
            }
            else
            {
                this.YUUKOU_BEGIN.Text = DateTime.Now.ToString();
                this.YUUKOU_BEGIN.ReadOnly = false;
                this.YUUKOU_END.ReadOnly = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YUUKOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.YUUKOU_BEGIN.Text != string.Empty)
            {
                this.YUUKOU_END.Text = this.YUUKOU_BEGIN.Text;
            }
        }
    }
}
