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
    internal class HanbaikanriPatternPanelLogic : IPatternPanelLogic
    {
        /// <summary>
        ///
        /// </summary>
        private HanbaikanriPatternPanel panel;

        /// <summary>
        ///
        /// </summary>
        private PatternDbAccessor dbAccessor;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        public HanbaikanriPatternPanelLogic(HanbaikanriPatternPanel panel)
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

            if (this.panel.chkDenshuKbnUkeire.Checked)
                denshuKbns.Add(DENSHU_KBN.UKEIRE);
            if (this.panel.chkDenshuKbnShukka.Checked)
                denshuKbns.Add(DENSHU_KBN.SHUKKA);
            if (this.panel.chkDenshuKbnUrSh.Checked)
                denshuKbns.Add(DENSHU_KBN.URIAGE_SHIHARAI);
            if (this.panel.chkDenshuKbnDainou.Checked)
                denshuKbns.Add(DENSHU_KBN.DAINOU);

            return denshuKbns;
        }

        /// <summary>
        ///
        /// </summary>
        public void EventInit()
        {
            this.panel.chkDenshuKbnUkeire.CheckedChanged -= this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnShukka.CheckedChanged -= this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnUrSh.CheckedChanged -= this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnDainou.CheckedChanged -= this.DenshuKbnCheckedChanged;
            this.panel.chkDenKbnUriage.CheckedChanged -= this.DenKbnCheckedChanged;
            this.panel.chkDenKbnShiharai.CheckedChanged -= this.DenKbnCheckedChanged;

            this.panel.chkDenshuKbnUkeire.CheckedChanged += this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnShukka.CheckedChanged += this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnUrSh.CheckedChanged += this.DenshuKbnCheckedChanged;
            this.panel.chkDenshuKbnDainou.CheckedChanged += this.DenshuKbnCheckedChanged;
            this.panel.chkDenKbnUriage.CheckedChanged += this.DenKbnCheckedChanged;
            this.panel.chkDenKbnShiharai.CheckedChanged += this.DenKbnCheckedChanged;
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
                    this.panel.chkDenshuKbnUkeire.Enabled = true;
                    this.panel.chkDenshuKbnShukka.Enabled = true;
                    this.panel.chkDenshuKbnUrSh.Enabled = true;
                    this.panel.chkDenshuKbnDainou.Enabled = true;
                    this.panel.chkDenKbnUriage.Enabled = true;
                    this.panel.chkDenKbnShiharai.Enabled = true;
                    this.panel.txtTorihikiKbn.Enabled = true;
                    this.panel.rdoTorihikiKbn1.Enabled = true;
                    this.panel.rdoTorihikiKbn2.Enabled = true;
                    this.panel.rdoTorihikiKbn3.Enabled = true;
                    this.panel.txtKakuteiKbn.Enabled = true;
                    this.panel.rdoKakuteiKbn1.Enabled = true;
                    this.panel.rdoKakuteiKbn2.Enabled = true;
                    this.panel.rdoKakuteiKbn3.Enabled = true;
                    this.panel.txtShimeKbn.Enabled = true;
                    this.panel.rdoShimeKbn1.Enabled = true;
                    this.panel.rdoShimeKbn2.Enabled = true;
                    this.panel.rdoShimeKbn3.Enabled = true;
                    break;

                default:
                    this.panel.chkDenshuKbnUkeire.Enabled = false;
                    this.panel.chkDenshuKbnShukka.Enabled = false;
                    this.panel.chkDenshuKbnUrSh.Enabled = false;
                    this.panel.chkDenshuKbnDainou.Enabled = false;
                    this.panel.chkDenKbnUriage.Enabled = false;
                    this.panel.chkDenKbnShiharai.Enabled = false;
                    this.panel.txtTorihikiKbn.Enabled = false;
                    this.panel.rdoTorihikiKbn1.Enabled = false;
                    this.panel.rdoTorihikiKbn2.Enabled = false;
                    this.panel.rdoTorihikiKbn3.Enabled = false;
                    this.panel.txtKakuteiKbn.Enabled = false;
                    this.panel.rdoKakuteiKbn1.Enabled = false;
                    this.panel.rdoKakuteiKbn2.Enabled = false;
                    this.panel.rdoKakuteiKbn3.Enabled = false;
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
                    this.panel.chkDenshuKbnUkeire.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_UKEIRE.IsTrue;
                    this.panel.chkDenshuKbnShukka.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_SHUKKA.IsTrue;
                    this.panel.chkDenshuKbnUrSh.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_UR_SH.IsTrue;
                    this.panel.chkDenshuKbnDainou.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_DAINOU.IsTrue;
                    this.panel.chkDenKbnUriage.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue;
                    this.panel.chkDenKbnShiharai.Checked = callForm.Pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue;
                    this.panel.txtTorihikiKbn.Text =
                        callForm.Pattern.CsvPatternHanbaikanri.TORIHIKI_KBN.IsNull ?
                        "3" : callForm.Pattern.CsvPatternHanbaikanri.TORIHIKI_KBN.Value.ToString();
                    this.panel.txtKakuteiKbn.Text =
                        callForm.Pattern.CsvPatternHanbaikanri.KAKUTEI_KBN.IsNull ?
                        "3" : callForm.Pattern.CsvPatternHanbaikanri.KAKUTEI_KBN.Value.ToString();
                    this.panel.txtShimeKbn.Text =
                        callForm.Pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU.IsNull ?
                        "3" : callForm.Pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU.Value.ToString();
                    break;

                default:
                    this.panel.chkDenshuKbnUkeire.Checked = true;
                    this.panel.chkDenshuKbnShukka.Checked = true;
                    this.panel.chkDenshuKbnUrSh.Checked = true;
                    this.panel.chkDenshuKbnDainou.Checked = true;
                    this.panel.chkDenKbnUriage.Checked = true;
                    this.panel.chkDenKbnShiharai.Checked = true;
                    this.panel.txtTorihikiKbn.Text = "3"; // 全て
                    this.panel.txtKakuteiKbn.Text = "3"; // 全て
                    this.panel.txtShimeKbn.Text = "3"; // 全て
                    break;
            }

            #endregion

            this.DenshuKbnSet();
            this.DenKbnSet();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="column"></param>
        public void PanelEntityCreate(PatternDto pattern)
        {
            var csvPatternHanbaikanri = new M_OUTPUT_CSV_PATTERN_HANBAIKANRI();

            csvPatternHanbaikanri.SYSTEM_ID = pattern.CsvPattern.SYSTEM_ID;
            csvPatternHanbaikanri.SEQ = pattern.CsvPattern.SEQ;
            csvPatternHanbaikanri.DENPYOU_SHURUI_UKEIRE = this.panel.chkDenshuKbnUkeire.Checked;
            csvPatternHanbaikanri.DENPYOU_SHURUI_SHUKKA = this.panel.chkDenshuKbnShukka.Checked;
            csvPatternHanbaikanri.DENPYOU_SHURUI_UR_SH = this.panel.chkDenshuKbnUrSh.Checked;
            csvPatternHanbaikanri.DENPYOU_SHURUI_DAINOU = this.panel.chkDenshuKbnDainou.Checked;
            csvPatternHanbaikanri.DENPYOU_KBN_URIAGE = this.panel.chkDenKbnUriage.Checked;
            csvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI = this.panel.chkDenKbnShiharai.Checked;
            csvPatternHanbaikanri.TORIHIKI_KBN = short.Parse(this.panel.txtTorihikiKbn.Text);
            csvPatternHanbaikanri.KAKUTEI_KBN = short.Parse(this.panel.txtKakuteiKbn.Text);
            csvPatternHanbaikanri.SHIME_SHORI_JOUKYOU = short.Parse(this.panel.txtShimeKbn.Text);

            pattern.CsvPatternHanbaikanri = csvPatternHanbaikanri;
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
                    callForm.Pattern.CsvPatternHanbaikanri = this.dbAccessor.GetCsvPatternHanbaikanri(callForm.Pattern.CsvPattern);
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
            return this.dbAccessor.InsertCsvPatternHanbaikanri(pattern.CsvPatternHanbaikanri);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenKbnCheckedChanged(object sender, EventArgs e)
        {
            this.DenKbnSet();
            (sender as Control).Select();
        }

        /// <summary>
        ///
        /// </summary>
        private void DenshuKbnSet()
        {
            var i =
                (this.panel.chkDenshuKbnUkeire.Checked ? 1 : 0) +
                (this.panel.chkDenshuKbnShukka.Checked ? 1 : 0) +
                (this.panel.chkDenshuKbnUrSh.Checked ? 1 : 0) +
                (this.panel.chkDenshuKbnDainou.Checked ? 1 : 0);

            if (i > 0)
                this.panel.txtDenshuKbn.Text = i.ToString();
            else
                this.panel.txtDenshuKbn.Text = string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        private void DenKbnSet()
        {
            var i =
                (this.panel.chkDenKbnUriage.Checked ? 1 : 0) +
                (this.panel.chkDenKbnShiharai.Checked ? 1 : 0);

            switch (i)
            {
                case 1:
                    this.panel.txtDenKbn.Text = i.ToString();

                    #region 利用可否

                    this.panel.rdoTorihikiKbn1.Enabled = true;
                    this.panel.rdoTorihikiKbn2.Enabled = true;
                    this.panel.rdoShimeKbn1.Enabled = true;
                    this.panel.rdoShimeKbn2.Enabled = true;

                    #endregion

                    break;

                case 2:
                    this.panel.txtDenKbn.Text = i.ToString();

                    #region 利用可否

                    this.panel.rdoTorihikiKbn1.Enabled = false;
                    this.panel.rdoTorihikiKbn2.Enabled = false;
                    this.panel.rdoShimeKbn1.Enabled = false;
                    this.panel.rdoShimeKbn2.Enabled = false;

                    #endregion

                    #region 値設定

                    this.panel.rdoTorihikiKbn3.Checked = true;
                    this.panel.rdoShimeKbn3.Checked = true;

                    #endregion

                    break;

                default:
                    this.panel.txtDenKbn.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void PreRegistCheck()
        {
            this.panel.txtDenshuKbn.Enabled = true;
            this.panel.txtDenKbn.Enabled = true;
        }

        /// <summary>
        ///
        /// </summary>
        public void PostRegistCheck()
        {
            this.panel.txtDenshuKbn.Enabled = false;
            this.panel.txtDenKbn.Enabled = false;
        }
    }
}