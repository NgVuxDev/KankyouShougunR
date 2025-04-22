using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.CustomControl;
using r_framework.Utility;
using GrapeCity.Win.MultiRow;
using r_framework.Entity;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public String KyotenCd { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DataRow TargetRow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TargetCol { get; set; }
        public DateTime StandandDateTime { get; set; }

        private M_SYS_INFO sysInfoEntityTemp;
        /// <summary>
        /// 
        /// </summary>
        public bool HaisyaKubun { get; private set; }    // No.2544関連

        public UIForm(string sagyouDate, string kyotenCd, string shashuCd, string shashuName, DataRow targetRow, string targetCol, bool haisyaKubun, M_SYS_INFO sysInfo, string untenshaCd, string untenshaName)
            : base(WINDOW_ID.T_SAGYOBI_HENKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            this.KyotenCd = kyotenCd;
            this.SHASHU_CD.Text = shashuCd;
            this.SHASHU_NAME.Text = shashuName;
            this.TargetRow = targetRow;
            this.TargetCol = targetCol;
            this.HaisyaKubun = haisyaKubun;    // No.2544関連
            var dt = DateTime.Parse(sagyouDate);
            this.sysInfoEntityTemp = sysInfo;
            this.dtpSagyouDate.Value = dt + new TimeSpan(1, 0, 0, 0);  //ThangNguyen [Add] 20150721
            this.StandandDateTime = dt;
            this.UNTENSHA_CD.Text = untenshaCd;
            this.UNTENSHA_NAME.Text = untenshaName;
            //this.mcSagyouDate.SelectionStart = this.mcSagyouDate.SelectionEnd = dt + new TimeSpan(1, 0, 0, 0);    //ThangNguyen [Delete] 20150721
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Load(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic = new LogicClass(this);
                this.logic.sysInfoEntity = this.sysInfoEntityTemp;
                this.logic.WindowInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrHaisha_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.logic.mrHaisha_CellClick(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrHaisha_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.logic.mrHaisha_CellFormatting(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrHaisha_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
             this.logic.mrHaisha_CellBeginEdit(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public string beforeSagyouDate = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSagyouDate_Enter(object sender, EventArgs e)
        {
            beforeSagyouDate = dtpSagyouDate.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSagyouDate_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (beforeSagyouDate != dtpSagyouDate.Text)
                {
                    this.logic.selectedCell = null;
                    this.logic.SearchAndDisplay();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運転者CD変更後、該当運転者の明細行を先頭行にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            if (this.logic.CheckUntensha())
            {
                this.logic.JumpGridRowByCode();
            }
        }

        /// <summary>
        /// 車輛CD変更後、該当車輛の明細行を先頭行にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_Validated(object sender, EventArgs e)
        {
            this.logic.JumpGridRowByCode();
        }
    }
}
