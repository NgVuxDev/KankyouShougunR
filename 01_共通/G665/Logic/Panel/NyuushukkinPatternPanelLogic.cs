using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Logic.Panel
{
    internal class NyuushukkinPatternPanelLogic : IPatternPanelLogic
    {
        /// <summary>
        ///
        /// </summary>
        private NyuushukkinPatternPanel panel;

        /// <summary>
        ///
        /// </summary>
        private PatternDbAccessor dbAccessor;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        public NyuushukkinPatternPanelLogic(NyuushukkinPatternPanel panel)
        {
            this.panel = panel;
            this.dbAccessor = new PatternDbAccessor();

            this.msgLogic = new MessageBoxShowLogic();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<DENSHU_KBN> DenshuKbnsCreate()
        {
            var denshuKbns = new List<DENSHU_KBN>();

            if (this.panel.chkDenshuKbnNyuukin.Checked)
                denshuKbns.Add(DENSHU_KBN.NYUUKIN);
            if (this.panel.chkDenshuKbnShukkin.Checked)
                denshuKbns.Add(DENSHU_KBN.SHUKKIN);

            return denshuKbns;
        }

        /// <summary>
        ///
        /// </summary>
        public void EventInit()
        {
            this.panel.chkDenshuKbnNyuukin.CheckedChanged -= this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnShukkin.CheckedChanged -= this.DenshuKbnCheckedChanged;

            this.panel.chkDenshuKbnNyuukin.CheckedChanged += this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnShukkin.CheckedChanged += this.DenshuKbnCheckedChanged;
        }

        /// <summary>
        ///
        /// </summary>
        public void PanelSet()
        {
            var callForm = this.panel.pnlDummy.FindForm() as PatternForm;
            if (callForm == null)
                return;

            #region 利用可否

            switch (callForm.WindowType)
            {
                case r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG:
                case r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.panel.chkDenshuKbnNyuukin.Enabled = true;
                    this.panel.chkDenshuKbnShukkin.Enabled = true;
                    this.panel.txtShimeKbn.Enabled = true;
                    this.panel.rdoShimeKbn1.Enabled = true;
                    this.panel.rdoShimeKbn2.Enabled = true;
                    this.panel.rdoShimeKbn3.Enabled = true;
                    break;

                default:
                    this.panel.chkDenshuKbnNyuukin.Enabled = false;
                    this.panel.chkDenshuKbnShukkin.Enabled = false;
                    this.panel.txtShimeKbn.Enabled = false;
                    this.panel.rdoShimeKbn1.Enabled = false;
                    this.panel.rdoShimeKbn2.Enabled = false;
                    this.panel.rdoShimeKbn3.Enabled = false;
                    break;
            }

            #endregion

            #region 値設定

            switch (callForm.WindowType)
            {
                case r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case r_framework.Const.WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.panel.chkDenshuKbnNyuukin.Checked = callForm.Pattern.CsvPatternNyuushukkin.DENPYOU_SHURUI_NYUUKIN.IsTrue;
                    this.panel.chkDenshuKbnShukkin.Checked = callForm.Pattern.CsvPatternNyuushukkin.DENPYOU_SHURUI_SHUKKIN.IsTrue;
                    this.panel.txtShimeKbn.Text =
                        callForm.Pattern.CsvPatternNyuushukkin.SHIME_SHORI_JOUKYOU.IsNull ?
                        "3" : callForm.Pattern.CsvPatternNyuushukkin.SHIME_SHORI_JOUKYOU.Value.ToString();
                    break;

                default:
                    this.panel.chkDenshuKbnNyuukin.Checked = true;
                    this.panel.chkDenshuKbnShukkin.Checked = true;
                    this.panel.txtShimeKbn.Text = "3"; // 全て
                    break;
            }

            #endregion

            this.DenshuKbnSet();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="column"></param>
        public void PanelEntityCreate(PatternDto pattern)
        {
            var csvPatternNyuushukkin = new M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN();

            csvPatternNyuushukkin.SYSTEM_ID = pattern.CsvPattern.SYSTEM_ID;
            csvPatternNyuushukkin.SEQ = pattern.CsvPattern.SEQ;
            csvPatternNyuushukkin.DENPYOU_SHURUI_NYUUKIN = this.panel.chkDenshuKbnNyuukin.Checked;
            csvPatternNyuushukkin.DENPYOU_SHURUI_SHUKKIN = this.panel.chkDenshuKbnShukkin.Checked;
            csvPatternNyuushukkin.SHIME_SHORI_JOUKYOU = short.Parse(this.panel.txtShimeKbn.Text);

            pattern.CsvPatternNyuushukkin = csvPatternNyuushukkin;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int PanelSearch()
        {
            var callForm = this.panel.pnlDummy.FindForm() as PatternForm;
            if (callForm == null)
                return 0;

            switch (callForm.WindowType)
            {
                case r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case r_framework.Const.WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    callForm.Pattern.CsvPatternNyuushukkin = this.dbAccessor.GetCsvPatternNyuushukkin(callForm.Pattern.CsvPattern);
                    return 1;

                default:
                    return 0;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int PanelNewRegist(bool errorFlag, PatternDto pattern)
        {
            return this.dbAccessor.InsertCsvPatternNyuushukkin(pattern.CsvPatternNyuushukkin);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenshuKbnCheckedChanged(object sender, EventArgs e)
        {
            this.DenshuKbnSet();
            (sender as Control).Select();
        }

        /// <summary>
        ///
        /// </summary>
        private void DenshuKbnSet()
        {
            var i =
                (this.panel.chkDenshuKbnNyuukin.Checked ? 1 : 0) +
                (this.panel.chkDenshuKbnShukkin.Checked ? 1 : 0);

            if (i > 0)
                this.panel.txtDenshuKbn.Text = i.ToString();
            else
                this.panel.txtDenshuKbn.Text = string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        public void PreRegistCheck()
        {
            this.panel.txtDenshuKbn.Enabled = true;
        }

        /// <summary>
        ///
        /// </summary>
        public void PostRegistCheck()
        {
            this.panel.txtDenshuKbn.Enabled = false;
        }
    }
}