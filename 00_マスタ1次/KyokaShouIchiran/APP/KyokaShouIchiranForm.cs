using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace KyokaShouIchiran
{
    [Implementation]
    public partial class KyokaShouIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        private LogicClass KyokaShouIchiranLogic;
        private Boolean isLoaded = false;
        private bool isShown = false;
        internal string oldGyoushaCd = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="denshuKbn"></param>
        /// <param name="headerForm"></param>
        public KyokaShouIchiranForm(DENSHU_KBN denshuKbn, HeaderForm headerForm)
            : base(denshuKbn, false)
        {
            try
            {
                this.InitializeComponent();
                this.ShainCd = SystemProperty.Shain.CD;
                this.DenshuKbn = denshuKbn;
                this.KyokaShouIchiranLogic = new LogicClass(this);
                this.SetHiddenColumns(
                    this.KyokaShouIchiranLogic.HIDDEN_KYOKA_KBN,
                    this.KyokaShouIchiranLogic.HIDDEN_GYOUSHA_CD,
                    this.KyokaShouIchiranLogic.HIDDEN_GENBA_CD,
                    this.KyokaShouIchiranLogic.HIDDEN_CHIIKI_CD
                    );
                this.KyokaShouIchiranLogic.SetHeader(headerForm);
                QuillInjector.GetInstance().Inject(this);
                isLoaded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("KyokaShouIchiranForm", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                if (isLoaded == false)
                {
                    this.KyokaShouIchiranLogic.WindowInit();
                }
                this.PatternReload(!this.isLoaded);
                if (!this.DesignMode)
                {
                    this.customDataGridView1.DataSource = null;
                    if (this.Table != null)
                    {
                        this.logic.CreateDataGridView(this.Table);
                    }
                }
                this.isLoaded = true;
                this.customSortHeader1.ClearCustomSortSetting();
                this.customSearchHeader1.ClearCustomSearchSetting();
                if (this.customDataGridView1 != null)
                {
                    this.KyokaShouIchiranLogic.headForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.KyokaShouIchiranLogic.headForm.ReadDataNumber.Text = "0";
                }
                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OnLoad", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void ShowData()
        {
            try
            {
                if (!this.DesignMode)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ShowData", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.HIDUKE_TO.Text = this.HIDUKE_FROM.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            if (this.GYOUSHA_CD.Text != this.oldGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void PopupBeforeGyoushaCode()
        {
            this.oldGyoushaCd = this.GYOUSHA_CD.Text;
        }      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.KyokaShouIchiranLogic.CheckGyoushaCd())
            {
                e.Cancel = true;
            }
            else if (this.oldGyoushaCd != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.KyokaShouIchiranLogic.CheckGenbaCd())
            {
                e.Cancel = true;
            }
        }      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.oldGyoushaCd = this.GYOUSHA_CD.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOKA_KBN_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.KYOKA_KBN.Text))
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Enabled = false;
                this.GENBA_SEARCH_BUTTON.Enabled = false;
            }
            else
            {
                this.GENBA_CD.Enabled = true;
                this.GENBA_SEARCH_BUTTON.Enabled = true;
            }
        }
    }
}