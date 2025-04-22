using System;
using System.Collections.Generic;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;

namespace SagyouBiPopup
{
    public partial class UIForm : SuperForm
    {
        #region Param
        private LogicClass logic = null;
        private bool isShown = false;
        public string GyoushaCd { get; set; }
        public string GyoushaName { get; set; }
        public string GenbaCd { get; set; }
        public string GenbaName { get; set; }
        public string UnpanGyoushaCd { get; set; }
        public string UnpanGyoushaName { get; set; }
        public string ShashuCd { get; set; }
        public string ShashuName { get; set; }
        public string SharyouCd { get; set; }
        public string SharyouName { get; set; }
        public string UntenshaCd { get; set; }
        public string UntenshaName { get; set; }
        public List<string> Sagyoubi { get; set; }
        public DateTime InOutDate { get; set; }
        #endregion

        #region Form
        /// <summary>
        /// 
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.SAGYOUBI_NYUURYOKU, WINDOW_TYPE.NONE)
        {
            LogUtility.DebugMethodStart();
            this.InitializeComponent();
            this.logic = new LogicClass(this);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);
            this.logic.WindowInit();
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
            this.dgvSagyouBi1.Focus();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F1 - F12
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            this.logic.SetDataNextAndPres(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            this.logic.SetDataNextAndPres();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            var dt1 = this.dgvSagyouBi1.DataSource as DataTable;
            if (dt1 != null)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    row["CHECK1"] = false;
                }
            }
            this.dgvSagyouBi1.DataSource = dt1;
            var dt2 = this.dgvSagyouBi2.DataSource as DataTable;
            if (dt2 != null)
            {
                foreach (DataRow row in dt2.Rows)
                {
                    row["CHECK2"] = false;
                }
            }
            this.dgvSagyouBi2.DataSource = dt2;
            var dt3 = this.dgvSagyouBi3.DataSource as DataTable;
            if (dt3 != null)
            {
                foreach (DataRow row in dt3.Rows)
                {
                    row["CHECK3"] = false;
                }
            }
            this.dgvSagyouBi3.DataSource = dt3;
            var dt4 = this.dgvSagyouBi4.DataSource as DataTable;
            if (dt4 != null)
            {
                foreach (DataRow row in dt4.Rows)
                {
                    row["CHECK4"] = false;
                }
            }
            this.dgvSagyouBi4.DataSource = dt4;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc9_Clicked(object sender, EventArgs e)
        {
            this.Sagyoubi = new List<string>();
            var dt1 = this.dgvSagyouBi1.DataSource as DataTable;
            if (dt1 != null)
            {
                this.logic.CreateListSagyouBi(dt1, 1);
            }
            var dt2 = this.dgvSagyouBi2.DataSource as DataTable;
            if (dt2 != null)
            {
                this.logic.CreateListSagyouBi(dt2, 2);
            }
            var dt3 = this.dgvSagyouBi3.DataSource as DataTable;
            if (dt3 != null)
            {
                this.logic.CreateListSagyouBi(dt3, 3);
            }
            var dt4 = this.dgvSagyouBi4.DataSource as DataTable;
            if (dt4 != null)
            {
                this.logic.CreateListSagyouBi(dt4, 4);
            }
            this.Close();
            this.logic.parentForm.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.Sagyoubi = null;
            this.Close();
            this.logic.parentForm.Close();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SagyouBi1 - 4
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSagyouBi1_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.dgvSagyouBi1.EndEdit();
            var cellName = this.dgvSagyouBi1.Columns[e.ColumnIndex].Name;
            if (cellName == "CHECK1")
            {
                var checkbox = (bool)this.dgvSagyouBi1[e.ColumnIndex, e.RowIndex].Value;
                if (checkbox)
                {
                    var sagyoubi = DateTime.Parse(this.dgvSagyouBi1["SA_GYOU_BI1", e.RowIndex].Value.ToString());
                    if (this.logic.CheckHanNyuusaki(sagyoubi))
                    {
                        this.dgvSagyouBi1[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckSharyou(sagyoubi))
                    {
                        this.dgvSagyouBi1[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckUntensha(sagyoubi))
                    {
                        this.dgvSagyouBi1[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSagyouBi2_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.dgvSagyouBi2.EndEdit();
            var cellName = this.dgvSagyouBi2.Columns[e.ColumnIndex].Name;
            if (cellName == "CHECK2")
            {
                var checkbox = (bool)this.dgvSagyouBi2[e.ColumnIndex, e.RowIndex].Value;
                if (checkbox)
                {
                    var sagyoubi = DateTime.Parse(this.dgvSagyouBi2["SA_GYOU_BI2", e.RowIndex].Value.ToString());
                    if (this.logic.CheckHanNyuusaki(sagyoubi))
                    {
                        this.dgvSagyouBi2[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckSharyou(sagyoubi))
                    {
                        this.dgvSagyouBi2[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckUntensha(sagyoubi))
                    {
                        this.dgvSagyouBi2[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSagyouBi3_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.dgvSagyouBi3.EndEdit();
            var cellName = this.dgvSagyouBi3.Columns[e.ColumnIndex].Name;
            if (cellName == "CHECK3")
            {
                var checkbox = (bool)this.dgvSagyouBi3[e.ColumnIndex, e.RowIndex].Value;
                if (checkbox)
                {
                    var sagyoubi = DateTime.Parse(this.dgvSagyouBi3["SA_GYOU_BI3", e.RowIndex].Value.ToString());
                    if (this.logic.CheckHanNyuusaki(sagyoubi))
                    {
                        this.dgvSagyouBi3[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckSharyou(sagyoubi))
                    {
                        this.dgvSagyouBi3[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckUntensha(sagyoubi))
                    {
                        this.dgvSagyouBi3[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSagyouBi4_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.dgvSagyouBi4.EndEdit();
            var cellName = this.dgvSagyouBi4.Columns[e.ColumnIndex].Name;
            if (cellName == "CHECK4")
            {
                var checkbox = (bool)this.dgvSagyouBi4[e.ColumnIndex, e.RowIndex].Value;
                if (checkbox)
                {
                    var sagyoubi = DateTime.Parse(this.dgvSagyouBi4["SA_GYOU_BI4", e.RowIndex].Value.ToString());
                    if (this.logic.CheckHanNyuusaki(sagyoubi))
                    {
                        this.dgvSagyouBi4[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckSharyou(sagyoubi))
                    {
                        this.dgvSagyouBi4[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    else if (this.logic.CheckUntensha(sagyoubi))
                    {
                        this.dgvSagyouBi4[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                }
            }
        }
        #endregion
    }
}
