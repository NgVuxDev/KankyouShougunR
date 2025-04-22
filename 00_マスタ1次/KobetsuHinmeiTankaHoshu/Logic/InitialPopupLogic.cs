using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using KobetsuHinmeiTankaHoshu.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace KobetsuHinmeiTankaHoshu.Logic
{
    public class InitialPopupFormLogic
    {
        private InitialPopupForm form;
        private static readonly string ButtonInfoXmlPath = "KobetsuHinmeiTankaHoshu.Setting.PopupButtonSetting.xml";
        public InitialPopupFormLogic(InitialPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            try
            {
                this.ButtonInit();
                this.EventInit();
                this.Init();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void EventInit()
        {
            this.form.bt_func8.Click += new EventHandler(this.form.Reflection);
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
        }
        /// <summary>
        /// 
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }
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
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ElementDecision()
        {
            try
            {
                if (this.inputCheck())
                {
                    return true;
                }
                if (!this.CheckDataCopy())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool inputCheck()
        {
            bool errorFlg = false;
            if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))//157931
            {
                this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "取引先また業者");//157931
                this.form.GYOUSHA_CD.Focus();
                return true;
            }
            if (string.IsNullOrEmpty(this.form.YUUKOU_BEGIN.Text) && !this.form.YUUKOU_BEGIN.ReadOnly)
            {
                this.form.YUUKOU_BEGIN.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "適用開始日");
                this.form.YUUKOU_BEGIN.Focus();
                return true;
            }
            if (!string.IsNullOrEmpty(this.form.YUUKOU_BEGIN.Text) &&
                !string.IsNullOrEmpty(this.form.YUUKOU_END.Text))
            {
                DateTime dtpFrom = DateTime.Parse(this.form.YUUKOU_BEGIN.Value.ToString());
                DateTime dtpTo = DateTime.Parse(this.form.YUUKOU_END.Value.ToString());
                int diff = dtpFrom.CompareTo(dtpTo);
                if (0 < diff)
                {
                    this.form.YUUKOU_BEGIN.IsInputErrorOccured = true;
                    this.form.YUUKOU_END.IsInputErrorOccured = true;
                    string[] errorMsg = { "適用開始日", "適用終了日" };
                    this.form.errmessage.MessageBoxShow("E030", errorMsg);
                    this.form.YUUKOU_BEGIN.Focus();
                    return true;
                }
            }
            return errorFlg;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            var date = DateTime.Now;
            this.form.YUUKOU_BEGIN.Text = date.ToString();
            this.form.YUUKOU_END.Text = string.Empty;
            this.form.TOROKU_HOUHOU_KBN.Text = "2";
            if (!string.IsNullOrEmpty(this.form.TorihikisakiCd))
            {
                var tori = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDataByCd(this.form.TorihikisakiCd);
                if (tori != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = tori.TORIHIKISAKI_CD;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = tori.TORIHIKISAKI_NAME_RYAKU;
                }
            }
            if (!string.IsNullOrEmpty(this.form.GyoushaCd))
            {
                var gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.GyoushaCd);
                if (gyousha != null)
                {
                    this.form.GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckDataCopy()
        {
            this.form.OutListSysId = new List<string>();
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT * FROM ");
            sql.AppendLine(" M_KOBETSU_HINMEI_TANKA ");
            sql.AppendLine(" WHERE DELETE_FLG = 0 ");
            sql.AppendLine(" AND DENPYOU_KBN_CD = " + this.form.DenpyouKbnCd);
            sql.AppendLine(" AND TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "'");
            sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'");
            sql.AppendLine(" AND GENBA_CD = '" + this.form.GENBA_CD.Text + "'");
            sql.AppendLine(" AND TEKIYOU_BEGIN > '" + DateTime.Now.ToShortDateString() + "'");
            var tmp = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDateForStringSql(sql.ToString());
            if (tmp != null && tmp.Rows.Count > 0)
            {
                foreach (DataRow row in tmp.Rows)
                {
                    this.form.OutListSysId.Add(row["SYS_ID"].ToString());
                }
                if ("2".Equals(this.form.TOROKU_HOUHOU_KBN.Text))
                {
                    this.form.errmessage.MessageBoxShowError("入力した適用開始日より、未来日の個別単価があります。確認をおこなってください。");
                    return false;
                }
            }
            sql = new StringBuilder();
            sql.AppendLine(" SELECT * FROM ");
            sql.AppendLine(" M_KOBETSU_HINMEI_TANKA ");
            sql.AppendLine(" WHERE DELETE_FLG = 0 ");
            sql.AppendLine(" AND DENPYOU_KBN_CD = " + this.form.DenpyouKbnCd);
            sql.AppendLine(" AND TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "'");
            sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'");
            sql.AppendLine(" AND GENBA_CD = '" + this.form.GENBA_CD.Text + "'");
            sql.AppendLine(" AND ((TEKIYOU_BEGIN <= '" + DateTime.Now.ToShortDateString() + "' ");
            sql.AppendLine(" AND TEKIYOU_END >= '" + DateTime.Now.ToShortDateString() + "') ");
            sql.AppendLine(" OR (TEKIYOU_BEGIN <= '" + DateTime.Now.ToShortDateString() + "' ");
            sql.AppendLine(" AND TEKIYOU_END IS NULL )) ");
            tmp = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDateForStringSql(sql.ToString());
            if (tmp != null && tmp.Rows.Count > 0)
            {
                foreach (DataRow row in tmp.Rows)
                {
                    this.form.OutListSysId.Add(row["SYS_ID"].ToString());
                }
                if ("2".Equals(this.form.TOROKU_HOUHOU_KBN.Text))
                {
                    this.form.errmessage.MessageBoxShowError("適用中の単価が登録されています。確認をおこなってください。");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool SearchTorihikisakiName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_CD.Text))
                {
                    var tori = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (tori != null)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = tori.TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            this.form.errmessage.MessageBoxShow("E020", "取引先");
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchTorihikisakiName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisakiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool SearchTorihikisakiByGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    var gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyousha != null)
                    {
                        if (gyousha.TORIHIKISAKI_UMU_KBN == 1)
                        {
                            var tori = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDataByCd(gyousha.TORIHIKISAKI_CD);
                            if (tori != null)
                            {
                                this.form.TORIHIKISAKI_CD.Text = tori.TORIHIKISAKI_CD;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = tori.TORIHIKISAKI_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchTorihikisakiByGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisakiByGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool SearchGenbaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    var genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
                    if (genba != null)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                        if (!this.form.beforGenbaCd.Equals(this.form.GENBA_CD.Text))
                        {
                            var tori = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDataByCd(genba.TORIHIKISAKI_CD);
                            if (tori != null)
                            {
                                this.form.TORIHIKISAKI_CD.Text = tori.TORIHIKISAKI_CD;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = tori.TORIHIKISAKI_NAME_RYAKU;
                            }
                        }
                    }

                    this.form.beforGyoushaCd = this.form.GYOUSHA_CD.Text;
                    this.form.beforGenbaCd = this.form.GENBA_CD.Text;
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool SearchGyoushaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    if (this.form.beforGyoushaCd.Equals(this.form.GYOUSHA_CD.Text))
                    {
                        return false;
                    }
                    this.SearchGenbaName(null);
                }
                else if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    var gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyousha != null)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        if (!this.form.beforGyoushaCd.Equals(this.form.GYOUSHA_CD.Text))
                        {
                            if (gyousha.TORIHIKISAKI_UMU_KBN == 1)
                            {
                                var tori = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDataByCd(gyousha.TORIHIKISAKI_CD);
                                if (tori != null)
                                {
                                    this.form.TORIHIKISAKI_CD.Text = tori.TORIHIKISAKI_CD;
                                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = tori.TORIHIKISAKI_NAME_RYAKU;
                                }
                                else
                                {
                                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                                }
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            }
                        }
                        this.form.beforGyoushaCd = this.form.GYOUSHA_CD.Text;
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                            this.form.errmessage.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba()
        {
            try
            {
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                bool ren = true;
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    if (String.IsNullOrEmpty(gyoushaCd))
                    {
                        this.form.errmessage.MessageBoxShow("E051", "業者");
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        this.form.GENBA_CD.SelectAll();
                        ren = false;
                    }
                    else if (!string.IsNullOrEmpty(genbaCd))
                    {
                        var genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
                        if (genba == null)
                        {
                            this.form.errmessage.MessageBoxShow("E020", "現場");

                            this.form.GENBA_CD.Focus();
                            this.form.GENBA_CD.SelectAll();
                            ren = false;
                        }
                    }
                    if (ren)
                    {
                        var gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.GYOUSHA_CD.Text);
                        if (null == gyousha)
                        {
                            this.form.errmessage.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            this.form.GYOUSHA_CD.SelectAll();
                            ren = false;
                        }
                    }
                }
                return ren;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ErrorCheckGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ErrorCheckGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
    }
}