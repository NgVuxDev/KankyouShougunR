using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace SagyouBiPopup
{
    internal class LogicClass
    {
        #region Param
        private const string buttonInfoXmlPath = "SagyouBiPopup.Setting.ButtonSetting.xml";
        private UIForm form;
        private IM_GYOUSHADao gyoushaDao;
        internal MessageBoxShowLogic errmessage;
        internal DateTime dtDateFrom;
        internal DateTime dtDateTo;
        internal BusinessBaseForm parentForm;
        #endregion

        #region Form
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public LogicClass(UIForm form)
        {
            LogUtility.DebugMethodStart();
            this.form = form;
            this.errmessage = new MessageBoxShowLogic();          
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.dtDateFrom = this.form.InOutDate;
                this.dtDateTo = this.dtDateFrom.AddMonths(1).AddDays(-1);
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.ButtonInit();
                this.EventInit();
                if (this.form.Sagyoubi == null)
                {
                    this.form.Sagyoubi = new List<string>();
                }
                this.SetInitData();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var ret = buttonSetting.LoadButtonSetting(thisAssembly, buttonInfoXmlPath);
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            this.parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            this.parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            this.parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);
            this.parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        internal void SetInitData()
        {
            #region 条件
            if (!string.IsNullOrEmpty(this.form.GyoushaCd))
            {
                this.form.NIOROSHI_GYOUSHA_CD.Text = this.form.GyoushaCd;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = this.form.GyoushaName;
            }
            else
            {
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.form.GenbaCd))
            {
                this.form.NIOROSHI_GENBA_CD.Text = this.form.GenbaCd;
                this.form.NIOROSHI_GENBA_NAME.Text = this.form.GenbaName;
            }
            else
            {
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.form.UnpanGyoushaCd))
            {
                this.form.UNPAN_GYOUSHA_CD.Text = this.form.UnpanGyoushaCd;
                this.form.UNPAN_GYOUSHA_NAME.Text = this.form.UnpanGyoushaName;
            }
            else
            {
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.form.ShashuCd))
            {
                this.form.SHASHU_CD.Text = this.form.ShashuCd;
                this.form.SHASHU_NAME.Text = this.form.ShashuName;
            }
            else
            {
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.form.SharyouCd))
            {
                this.form.SHARYOU_CD.Text = this.form.SharyouCd;
                this.form.SHARYOU_NAME.Text = this.form.SharyouName;
            }
            else
            {
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.form.UntenshaCd))
            {
                this.form.UNTENSHA_CD.Text = this.form.UntenshaCd;
                this.form.UNTENSHA_NAME.Text = this.form.UntenshaName;
            }
            else
            {
                this.form.UNTENSHA_CD.Text = string.Empty;
                this.form.UNTENSHA_NAME.Text = string.Empty;
            }
            #endregion

            #region 作業日
            var dtSagyouBi1 = this.CreateSagyouBi1();
            for (var i = dtDateFrom; i <= dtDateTo; i = i.AddDays(1))
            {
                var row = dtSagyouBi1.NewRow();
                row["SA_GYOU_BI1"] = i;
                if (this.form.Sagyoubi.Contains(i.ToString()))
                {
                    row["CHECK1"] = true;
                }
                else
                {
                    row["CHECK1"] = false;
                }
                dtSagyouBi1.Rows.Add(row);
            }
            this.form.dgvSagyouBi1.DataSource = dtSagyouBi1;

            var dtSagyouBi2 = this.CreateSagyouBi2();
            var from1 = dtDateFrom.AddMonths(1);
            var to1 = dtDateTo.AddMonths(1);
            for (var i = from1; i <= to1; i = i.AddDays(1))
            {
                var row = dtSagyouBi2.NewRow();
                row["SA_GYOU_BI2"] = i;
                if (this.form.Sagyoubi.Contains(i.ToString()))
                {
                    row["CHECK2"] = true;
                }
                else
                {
                    row["CHECK2"] = false;
                }
                dtSagyouBi2.Rows.Add(row);
            }
            this.form.dgvSagyouBi2.DataSource = dtSagyouBi2;

            var dtSagyouBi3 = this.CreateSagyouBi3();
            var from2 = dtDateFrom.AddMonths(2);
            var to2 = dtDateTo.AddMonths(2);
            for (var i = from2; i <= to2; i = i.AddDays(1))
            {
                var row = dtSagyouBi3.NewRow();
                row["SA_GYOU_BI3"] = i;
                if (this.form.Sagyoubi.Contains(i.ToString()))
                {
                    row["CHECK3"] = true;
                }
                else
                {
                    row["CHECK3"] = false;
                }
                dtSagyouBi3.Rows.Add(row);
            }
            this.form.dgvSagyouBi3.DataSource = dtSagyouBi3;

            var dtSagyouBi4 = this.CreateSagyouBi4();
            var from3 = dtDateFrom.AddMonths(3);
            var to3 = dtDateTo.AddMonths(3);
            for (var i = from3; i <= to3; i = i.AddDays(1))
            {
                var row = dtSagyouBi4.NewRow();
                row["SA_GYOU_BI4"] = i;
                if (this.form.Sagyoubi.Contains(i.ToString()))
                {
                    row["CHECK4"] = true;
                }
                else
                {
                    row["CHECK4"] = false;
                }
                dtSagyouBi4.Rows.Add(row);
            }
            this.form.dgvSagyouBi4.DataSource = dtSagyouBi4;
            #endregion
        }
        #endregion

        #region 作業日
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateSagyouBi1()
        {
            var dt = new DataTable();
            dt.Columns.Add("SA_GYOU_BI1");
            dt.Columns.Add("CHECK1", typeof(bool));
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateSagyouBi2()
        {
            var dt = new DataTable();
            dt.Columns.Add("SA_GYOU_BI2");
            dt.Columns.Add("CHECK2", typeof(bool));
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateSagyouBi3()
        {
            var dt = new DataTable();
            dt.Columns.Add("SA_GYOU_BI3");
            dt.Columns.Add("CHECK3", typeof(bool));
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateSagyouBi4()
        {
            var dt = new DataTable();
            dt.Columns.Add("SA_GYOU_BI4");
            dt.Columns.Add("CHECK4", typeof(bool));
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="index"></param>
        internal void CreateListSagyouBi(DataTable dt, int index = 1)
        {
            foreach (DataRow row in dt.Rows)
            {
                if ((bool)row["CHECK" + index])
                {
                    this.form.Sagyoubi.Add(DateTime.Parse(row["SA_GYOU_BI" + index].ToString()).ToString());
                }
            }
        }
        #endregion

        #region チェック
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        internal bool SagyouBiChecked(DataTable dt, int index = 1)
        {
            foreach (DataRow row in dt.Rows)
            {
                if ((bool)row["CHECK" + index])
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sagyoubi"></param>
        /// <returns></returns>
        internal bool CheckHanNyuusaki(DateTime sagyoubi)
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_HANNYUUSAKI ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.NIOROSHI_GYOUSHA_CD.Text + "'");
                sql.AppendLine(" AND GENBA_CD = '" + this.form.NIOROSHI_GENBA_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.errmessage.MessageBoxShowError("荷降現場が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sagyoubi"></param>
        /// <returns></returns>
        internal bool CheckSharyou(DateTime sagyoubi)
        {
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text) && !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_SHARYOU ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "'");
                sql.AppendLine(" AND SHARYOU_CD = '" + this.form.SHARYOU_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.errmessage.MessageBoxShowError("車輌が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sagyoubi"></param>
        /// <returns></returns>
        internal bool CheckUntensha(DateTime sagyoubi)
        {
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_UNTENSHA ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND SHAIN_CD = '" + this.form.UNTENSHA_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.errmessage.MessageBoxShowError("運転者が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isNext"></param>
        internal void SetDataNextAndPres(bool isNext = true)
        {
            #region メッセージ
            var dt1 = this.form.dgvSagyouBi1.DataSource as DataTable;
            if (dt1 == null)
            {
                return;
            }
            var flg = false;
            if (this.SagyouBiChecked(dt1, 1))
            {
                flg = true;
            }
            var dt2 = this.form.dgvSagyouBi2.DataSource as DataTable;
            if (dt2 == null)
            {
                return;
            }
            if (this.SagyouBiChecked(dt2, 2))
            {
                flg = true;
            }
            var dt3 = this.form.dgvSagyouBi3.DataSource as DataTable;
            if (dt3 == null)
            {
                return;
            }
            if (this.SagyouBiChecked(dt3, 3))
            {
                flg = true;
            }
            var dt4 = this.form.dgvSagyouBi4.DataSource as DataTable;
            if (dt4 == null)
            {
                return;
            }
            if (this.SagyouBiChecked(dt4, 4))
            {
                flg = true;
            }
            if (flg)
            {
                if (this.errmessage.MessageBoxShowConfirm("選択した作業日が破棄されますがよろしいですか？") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            #endregion

            #region 作業日
            if (isNext)
            {
                this.dtDateFrom = this.dtDateFrom.AddMonths(1);
                this.dtDateTo = this.dtDateFrom.AddMonths(1).AddDays(-1);
            }
            else
            {
                this.dtDateFrom = this.dtDateFrom.AddMonths(-1);
                this.dtDateTo = this.dtDateFrom.AddMonths(1).AddDays(-1);
            }
            this.form.InOutDate = this.dtDateFrom;
            var dtSagyouBi1 = this.CreateSagyouBi1();
            for (var i = dtDateFrom; i <= dtDateTo; i = i.AddDays(1))
            {
                var row = dtSagyouBi1.NewRow();
                row["SA_GYOU_BI1"] = i;
                row["CHECK1"] = false;
                dtSagyouBi1.Rows.Add(row);
            }
            this.form.dgvSagyouBi1.DataSource = dtSagyouBi1;

            var dtSagyouBi2 = this.CreateSagyouBi2();
            var from1 = dtDateFrom.AddMonths(1);
            var to1 = from1.AddMonths(1).AddDays(-1);
            for (var i = from1; i <= to1; i = i.AddDays(1))
            {
                var row = dtSagyouBi2.NewRow();
                row["SA_GYOU_BI2"] = i;
                row["CHECK2"] = false;
                dtSagyouBi2.Rows.Add(row);
            }
            this.form.dgvSagyouBi2.DataSource = dtSagyouBi2;

            var dtSagyouBi3 = this.CreateSagyouBi3();
            var from2 = dtDateFrom.AddMonths(2);
            var to2 = from2.AddMonths(1).AddDays(-1);
            for (var i = from2; i <= to2; i = i.AddDays(1))
            {
                var row = dtSagyouBi3.NewRow();
                row["SA_GYOU_BI3"] = i;
                row["CHECK3"] = false;
                dtSagyouBi3.Rows.Add(row);
            }
            this.form.dgvSagyouBi3.DataSource = dtSagyouBi3;

            var dtSagyouBi4 = this.CreateSagyouBi4();
            var from3 = dtDateFrom.AddMonths(3);
            var to3 = from3.AddMonths(1).AddDays(-1);
            for (var i = from3; i <= to3; i = i.AddDays(1))
            {
                var row = dtSagyouBi4.NewRow();
                row["SA_GYOU_BI4"] = i;
                row["CHECK4"] = false;
                dtSagyouBi4.Rows.Add(row);
            }
            this.form.dgvSagyouBi4.DataSource = dtSagyouBi4;
            #endregion
        }
        #endregion
    }
}
