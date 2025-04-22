using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Logic
{
    internal class JokenLogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        private JokenForm callForm;

        /// <summary>
        /// DBアクセス
        /// </summary>
        private MasterDbAccessor dbAccessor;

        /// <summary>
        ///
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        #endregion

        #region コンストラクタ

        /// <summary>
        ///
        /// </summary>
        /// <param name="callForm"></param>
        public JokenLogicClass(JokenForm callForm)
        {
            LogUtility.DebugMethodStart(callForm);

            this.callForm = callForm;
            this.dbAccessor = new MasterDbAccessor();
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region メソッド

        /// <summary>
        ///
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                // イベント初期化
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        private void EventInit()
        {
            #region 既存イベント解除

            this.callForm.bt_func9.Click -= this.callForm.bt_func9_Click;
            this.callForm.bt_func12.Click -= this.callForm.bt_func12_Click;

            #endregion

            #region 新しいイベントバインド

            // 検索実行ボタン(F9)イベント生成
            this.callForm.bt_func9.Click += this.callForm.bt_func9_Click;

            // キャンセルボタン(F12)イベント生成
            this.callForm.bt_func12.Click += this.callForm.bt_func12_Click;

            #endregion
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool JokenInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                #region 利用可否

                this.callForm.bt_func9.Enabled = true;
                this.callForm.bt_func12.Enabled = true;

                this.callForm.txtHaniKbn.Enabled = true;
                this.callForm.txtDateSpecify.Enabled = true;
                this.callForm.txtDateSpecify2.Enabled = true;
                this.callForm.txtKyotenCd.Enabled = true;
                this.callForm.txtTorihikisakiCdFrom.Enabled = true;
                this.callForm.txtTorihikisakiCdTo.Enabled = true;

                #endregion

                #region 値設定

                // 出力範囲により、各コントロールの利用可否を設定
                this.callForm.txtHaniKbn.Text = this.callForm.Joken.HaniKbn.ToString();
                this.HaniJokenChanged(true);

                // 日付範囲により、開始終了日付を設定
                this.callForm.txtDateSpecify2.Text = this.callForm.Joken.DateSpecify2.ToString();
                this.DateSpecify2Changed(true);

                // 一部コントロールは条件問わず常に設定
                this.callForm.txtKyotenCd.Text = this.callForm.Joken.KyotenCd;
                this.callForm.txtTorihikisakiCdFrom.Text = this.callForm.Joken.TorihikisakiCdFrom;
                this.callForm.txtTorihikisakiCdTo.Text = this.callForm.Joken.TorihikisakiCdTo;

                // 自動にCDと名称を設定するため、AutoFocusOutCheckを呼ぶ
                foreach (var control in this.callForm.allControl)
                    if (control is ICustomControl)
                        new AutoFocusOutCheckLogic(control as ICustomControl, this.callForm.allControl).AutoFocusOutCheck();

                this.GenbaCheckAndSetting(this.callForm.txtGenbaCdFrom, true);
                this.GenbaCheckAndSetting(this.callForm.txtGenbaCdTo, true);
                this.BankShitenCdCheckAndSetting(this.callForm.txtBankShitenCdFrom, true);
                this.BankShitenCdCheckAndSetting(this.callForm.txtBankShitenCdTo, true);

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("JokenInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 出力範囲変更
        /// </summary>
        /// <param name="isInit"></param>
        internal void HaniJokenChanged(bool isInit)
        {
            switch (this.callForm.txtHaniKbn.Text)
            {
                case "1":

                    #region 利用可否

                    this.callForm.rdoDateSpecify1.Enabled = true;
                    this.callForm.rdoDateSpecify2.Enabled = true;
                    this.callForm.rdoDateSpecify3.Enabled = true;
                    this.callForm.rdoDateSpecify4.Enabled = true;

                    this.callForm.txtGyoushaCdFrom.Enabled = true;
                    this.callForm.txtGyoushaCdTo.Enabled = true;
                    this.callForm.txtNyuukinsakiCdFrom.Enabled = false;
                    this.callForm.txtNyuukinsakiCdTo.Enabled = false;
                    this.callForm.txtBankCdFrom.Enabled = false;
                    this.callForm.txtBankCdTo.Enabled = false;
                    this.callForm.txtBankShitenCdFrom.Enabled = false;
                    this.callForm.txtBankShitenCdTo.Enabled = false;

                    this.callForm.btnGyoushaFrom.Enabled = true;
                    this.callForm.btnGyoushaTo.Enabled = true;
                    this.callForm.btnNyuukinsakiFrom.Enabled = false;
                    this.callForm.btnNyuukinsakiTo.Enabled = false;
                    this.callForm.btnBankFrom.Enabled = false;
                    this.callForm.btnBankTo.Enabled = false;
                    this.callForm.btnBankShitenFrom.Enabled = false;
                    this.callForm.btnBankShitenTo.Enabled = false;

                    #endregion

                    #region 値設定

                    if (isInit)
                    {
                        this.callForm.txtDateSpecify.Text = this.callForm.Joken.DateSpecify.ToString();
                        this.callForm.txtGyoushaCdFrom.Text = this.callForm.Joken.GyoushaCdFrom;
                        this.callForm.txtGyoushaCdTo.Text = this.callForm.Joken.GyoushaCdTo;

                        this.GyoushaCdChanged(isInit);
                    }

                    this.callForm.txtNyuukinsakiCdFrom.Text = string.Empty;
                    this.callForm.txtNyuukinsakiCdTo.Text = string.Empty;
                    this.callForm.txtBankCdFrom.Text = string.Empty;
                    this.callForm.txtBankCdTo.Text = string.Empty;
                    this.callForm.txtBankShitenCdFrom.Text = string.Empty;
                    this.callForm.txtBankShitenCdTo.Text = string.Empty;

                    this.callForm.txtNyuukinsakiNmFrom.Text = string.Empty;
                    this.callForm.txtNyuukinsakiNmTo.Text = string.Empty;
                    this.callForm.txtBankNmFrom.Text = string.Empty;
                    this.callForm.txtBankNmTo.Text = string.Empty;
                    this.callForm.txtBankShitenNmFrom.Text = string.Empty;
                    this.callForm.txtBankShitenNmTo.Text = string.Empty;

                    #endregion

                    break;

                case "2":

                    #region 利用可否

                    this.callForm.rdoDateSpecify1.Enabled = true;
                    this.callForm.rdoDateSpecify2.Enabled = false;
                    this.callForm.rdoDateSpecify3.Enabled = false;
                    this.callForm.rdoDateSpecify4.Enabled = true;

                    this.callForm.txtGyoushaCdFrom.Enabled = false;
                    this.callForm.txtGyoushaCdTo.Enabled = false;
                    this.callForm.txtGenbaCdFrom.Enabled = false;
                    this.callForm.txtGenbaCdTo.Enabled = false;
                    this.callForm.txtNyuukinsakiCdFrom.Enabled = true;
                    this.callForm.txtNyuukinsakiCdTo.Enabled = true;
                    this.callForm.txtBankCdFrom.Enabled = true;
                    this.callForm.txtBankCdTo.Enabled = true;

                    this.callForm.btnGyoushaFrom.Enabled = false;
                    this.callForm.btnGyoushaTo.Enabled = false;
                    this.callForm.btnGenbaFrom.Enabled = false;
                    this.callForm.btnGenbaTo.Enabled = false;
                    this.callForm.btnNyuukinsakiFrom.Enabled = true;
                    this.callForm.btnNyuukinsakiTo.Enabled = true;
                    this.callForm.btnBankFrom.Enabled = true;
                    this.callForm.btnBankTo.Enabled = true;

                    #endregion

                    #region 値設定

                    if (isInit)
                    {
                        if (this.callForm.Joken.DateSpecify == 2 || this.callForm.Joken.DateSpecify == 3)
                            this.callForm.txtDateSpecify.Text = "1";
                        else
                            this.callForm.txtDateSpecify.Text = this.callForm.Joken.DateSpecify.ToString();

                        this.callForm.txtNyuukinsakiCdFrom.Text = this.callForm.Joken.NyuukinsakiCdFrom;
                        this.callForm.txtNyuukinsakiCdTo.Text = this.callForm.Joken.NyuukinsakiCdTo;
                        this.callForm.txtBankCdFrom.Text = this.callForm.Joken.BankCdFrom;
                        this.callForm.txtBankCdTo.Text = this.callForm.Joken.BankCdTo;

                        this.BankCdChanged(isInit);
                    }
                    else
                    {
                        if (this.callForm.rdoDateSpecify2.Checked || this.callForm.rdoDateSpecify3.Checked)
                            this.callForm.txtDateSpecify.Text = "1";
                    }

                    this.callForm.rdoDateSpecify2.Checked = false;
                    this.callForm.rdoDateSpecify3.Checked = false;

                    this.callForm.txtGyoushaCdFrom.Text = string.Empty;
                    this.callForm.txtGyoushaCdTo.Text = string.Empty;
                    this.callForm.txtGenbaCdFrom.Text = string.Empty;
                    this.callForm.txtGenbaCdTo.Text = string.Empty;

                    this.callForm.txtGyoushaNmFrom.Text = string.Empty;
                    this.callForm.txtGyoushaNmTo.Text = string.Empty;
                    this.callForm.txtGenbaNmFrom.Text = string.Empty;
                    this.callForm.txtGenbaNmTo.Text = string.Empty;

                    #endregion

                    break;

                default:

                    #region 利用可否

                    this.callForm.rdoDateSpecify1.Enabled = false;
                    this.callForm.rdoDateSpecify2.Enabled = false;
                    this.callForm.rdoDateSpecify3.Enabled = false;
                    this.callForm.rdoDateSpecify4.Enabled = false;

                    this.callForm.txtGyoushaCdFrom.Enabled = false;
                    this.callForm.txtGyoushaCdTo.Enabled = false;
                    this.callForm.txtGenbaCdFrom.Enabled = false;
                    this.callForm.txtGenbaCdTo.Enabled = false;
                    this.callForm.txtNyuukinsakiCdFrom.Enabled = false;
                    this.callForm.txtNyuukinsakiCdTo.Enabled = false;
                    this.callForm.txtBankCdFrom.Enabled = false;
                    this.callForm.txtBankCdTo.Enabled = false;
                    this.callForm.txtBankShitenCdFrom.Enabled = false;
                    this.callForm.txtBankShitenCdTo.Enabled = false;

                    this.callForm.btnGyoushaFrom.Enabled = false;
                    this.callForm.btnGyoushaTo.Enabled = false;
                    this.callForm.btnGenbaFrom.Enabled = false;
                    this.callForm.btnGenbaTo.Enabled = false;
                    this.callForm.btnNyuukinsakiFrom.Enabled = false;
                    this.callForm.btnNyuukinsakiTo.Enabled = false;
                    this.callForm.btnBankFrom.Enabled = false;
                    this.callForm.btnBankTo.Enabled = false;
                    this.callForm.btnBankShitenFrom.Enabled = false;
                    this.callForm.btnBankShitenTo.Enabled = false;

                    #endregion

                    #region 値設定

                    this.callForm.txtDateSpecify.Text = string.Empty;
                    this.callForm.rdoDateSpecify1.Checked = false;
                    this.callForm.rdoDateSpecify2.Checked = false;
                    this.callForm.rdoDateSpecify3.Checked = false;
                    this.callForm.rdoDateSpecify4.Checked = false;

                    this.callForm.txtGyoushaCdFrom.Text = string.Empty;
                    this.callForm.txtGyoushaCdTo.Text = string.Empty;
                    this.callForm.txtGenbaCdFrom.Text = string.Empty;
                    this.callForm.txtGenbaCdTo.Text = string.Empty;
                    this.callForm.txtNyuukinsakiCdFrom.Text = string.Empty;
                    this.callForm.txtNyuukinsakiCdTo.Text = string.Empty;
                    this.callForm.txtBankCdFrom.Text = string.Empty;
                    this.callForm.txtBankCdTo.Text = string.Empty;
                    this.callForm.txtBankShitenCdFrom.Text = string.Empty;
                    this.callForm.txtBankShitenCdTo.Text = string.Empty;

                    this.callForm.txtGyoushaNmFrom.Text = string.Empty;
                    this.callForm.txtGyoushaNmTo.Text = string.Empty;
                    this.callForm.txtGenbaNmFrom.Text = string.Empty;
                    this.callForm.txtGenbaNmTo.Text = string.Empty;
                    this.callForm.txtNyuukinsakiNmFrom.Text = string.Empty;
                    this.callForm.txtNyuukinsakiNmTo.Text = string.Empty;
                    this.callForm.txtBankNmFrom.Text = string.Empty;
                    this.callForm.txtBankNmTo.Text = string.Empty;
                    this.callForm.txtBankShitenNmFrom.Text = string.Empty;
                    this.callForm.txtBankShitenNmTo.Text = string.Empty;

                    #endregion

                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isInit"></param>
        internal void DateSpecify2Changed(bool isInit)
        {
            switch (this.callForm.txtDateSpecify2.Text)
            {
                case "1":

                    #region 利用可否

                    this.callForm.dtpDateFrom.Enabled = false;
                    this.callForm.dtpDateTo.Enabled = false;

                    #endregion

                    #region 値設定

                    this.callForm.dtpDateFrom.Value = this.callForm.sysDate;
                    this.callForm.dtpDateTo.Value = this.callForm.sysDate;

                    #endregion

                    break;

                case "2":

                    #region 利用可否

                    this.callForm.dtpDateFrom.Enabled = false;
                    this.callForm.dtpDateTo.Enabled = false;

                    #endregion

                    #region 値設定

                    var sysDate = this.callForm.sysDate;
                    this.callForm.dtpDateFrom.Value = new DateTime(sysDate.Year, sysDate.Month, 1);
                    this.callForm.dtpDateTo.Value = new DateTime(sysDate.Year, sysDate.Month, 1).AddMonths(1).AddDays(-1);

                    #endregion

                    break;

                case "3":

                    #region 利用可否

                    this.callForm.dtpDateFrom.Enabled = true;
                    this.callForm.dtpDateTo.Enabled = true;

                    #endregion

                    #region 値設定

                    if (isInit)
                    {
                        this.callForm.dtpDateFrom.Value = this.callForm.Joken.DateFrom;
                        this.callForm.dtpDateTo.Value = this.callForm.Joken.DateTo;
                    }
                    else
                    {
                        this.callForm.dtpDateFrom.Text = string.Empty;
                        this.callForm.dtpDateTo.Text = string.Empty;
                    }

                    #endregion

                    break;

                default:

                    #region 利用可否

                    this.callForm.dtpDateFrom.Enabled = false;
                    this.callForm.dtpDateTo.Enabled = false;

                    #endregion

                    #region 値設定

                    this.callForm.dtpDateFrom.Text = string.Empty;
                    this.callForm.dtpDateTo.Text = string.Empty;

                    #endregion

                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="haniKbn"></param>
        internal void GyoushaCdChanged(bool isInit)
        {
            if (string.IsNullOrWhiteSpace(this.callForm.txtGyoushaCdFrom.Text) || string.IsNullOrWhiteSpace(this.callForm.txtGyoushaCdTo.Text) ||
                string.Compare(
                this.callForm.txtGyoushaCdFrom.Text.PadLeft(6, '0'),
                this.callForm.txtGyoushaCdTo.Text.PadLeft(6, '0'),
                this.callForm.txtGyoushaCdFrom.ChangeUpperCase && this.callForm.txtGyoushaCdTo.ChangeUpperCase
                ) != 0)
            {
                // 利用可否
                this.callForm.txtGenbaCdFrom.Enabled = false;
                this.callForm.txtGenbaCdTo.Enabled = false;
                this.callForm.btnGenbaFrom.Enabled = false;
                this.callForm.btnGenbaTo.Enabled = false;
                // 値設定
                this.callForm.txtGenbaCdFrom.Text = string.Empty;
                this.callForm.txtGenbaCdTo.Text = string.Empty;
                this.callForm.txtGenbaNmFrom.Text = string.Empty;
                this.callForm.txtGenbaNmTo.Text = string.Empty;
            }
            else
            {
                // 利用可否
                this.callForm.txtGenbaCdFrom.Enabled = true;
                this.callForm.txtGenbaCdTo.Enabled = true;
                this.callForm.btnGenbaFrom.Enabled = true;
                this.callForm.btnGenbaTo.Enabled = true;
                // 値設定
                if (isInit)
                {
                    this.callForm.txtGenbaCdFrom.Text = this.callForm.Joken.GenbaCdFrom;
                    this.callForm.txtGenbaCdTo.Text = this.callForm.Joken.GenbaCdTo;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="haniKbn"></param>
        internal void BankCdChanged(bool isInit)
        {
            if (string.IsNullOrWhiteSpace(this.callForm.txtBankCdFrom.Text) || string.IsNullOrWhiteSpace(this.callForm.txtBankCdTo.Text) ||
                string.Compare(
                this.callForm.txtBankCdFrom.Text.PadLeft(4, '0'),
                this.callForm.txtBankCdTo.Text.PadLeft(4, '0'),
                this.callForm.txtBankCdFrom.ChangeUpperCase && this.callForm.txtBankCdTo.ChangeUpperCase
                ) != 0)
            {
                // 利用可否
                this.callForm.txtBankShitenCdFrom.Enabled = false;
                this.callForm.txtBankShitenCdTo.Enabled = false;
                this.callForm.btnBankShitenFrom.Enabled = false;
                this.callForm.btnBankShitenTo.Enabled = false;
                // 値設定
                this.callForm.txtBankShitenCdFrom.Text = string.Empty;
                this.callForm.txtBankShitenCdTo.Text = string.Empty;
                this.callForm.txtBankShitenNmFrom.Text = string.Empty;
                this.callForm.txtBankShitenNmTo.Text = string.Empty;
            }
            else
            {
                // 利用可否
                this.callForm.txtBankShitenCdFrom.Enabled = true;
                this.callForm.txtBankShitenCdTo.Enabled = true;
                this.callForm.btnBankShitenFrom.Enabled = true;
                this.callForm.btnBankShitenTo.Enabled = true;
                // 値設定
                if (isInit)
                {
                    this.callForm.txtBankShitenCdFrom.Text = this.callForm.Joken.BankShitenCdFrom;
                    this.callForm.txtBankShitenCdTo.Text = this.callForm.Joken.BankShitenCdTo;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool JokenSave()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (ret)
                {
                    // 日付FromToチェックが日付単独チェック結果に影響があるので、チェック実行前にFromToチェックを外す
                    var dtpDateFromFromToCheck = this.callForm.dtpDateFrom.RegistCheckMethod.FirstOrDefault(x => x.CheckMethodName == "日付整合性チェック(From用)");
                    var dtpDateToFromToCheck = this.callForm.dtpDateTo.RegistCheckMethod.FirstOrDefault(x => x.CheckMethodName == "日付整合性チェック(To用)");
                    if (string.IsNullOrWhiteSpace(this.callForm.dtpDateFrom.Text) || string.IsNullOrWhiteSpace(this.callForm.dtpDateTo.Text))
                    {
                        if (dtpDateFromFromToCheck != null)
                            this.callForm.dtpDateFrom.RegistCheckMethod.Remove(dtpDateFromFromToCheck);
                        if (dtpDateToFromToCheck != null)
                            this.callForm.dtpDateTo.RegistCheckMethod.Remove(dtpDateToFromToCheck);
                    }

                    // AutoRegistCheckを利用し、全コントロールをチェック
                    if (new AutoRegistCheckLogic(this.callForm.allControl).AutoRegistCheck())
                    {
                        ret = false;

                        // 最初のエラーになったコントロールにフォーカス
                        foreach (var item in this.callForm.allControl.OrderBy(c => c.TabIndex))
                        {
                            if (item is ICustomAutoChangeBackColor && (item as ICustomAutoChangeBackColor).IsInputErrorOccured)
                            {
                                var custom = item as Control;
                                custom.Select();
                                if (!custom.TabStop)
                                    (custom.Parent ?? this.callForm).SelectNextControl(custom, true, true, true, true);

                                break;
                            }
                        }

                        // フォーカス当たるコントロールを確定した後で、CausesValidationをTRUEに
                        foreach (var item in this.callForm.allControl.Where(c => c is CustomTextBox))
                            item.CausesValidation = true;
                    }

                    // 日付FromToチェックを復元
                    if (dtpDateFromFromToCheck != null && !this.callForm.dtpDateFrom.RegistCheckMethod.Contains(dtpDateFromFromToCheck))
                        this.callForm.dtpDateFrom.RegistCheckMethod.Add(dtpDateFromFromToCheck);
                    if (dtpDateToFromToCheck != null && !this.callForm.dtpDateTo.RegistCheckMethod.Contains(dtpDateToFromToCheck))
                        this.callForm.dtpDateTo.RegistCheckMethod.Add(dtpDateToFromToCheck);
                }

                if (ret)
                {
                    this.callForm.Joken.HaniKbn = int.Parse(this.callForm.txtHaniKbn.Text);
                    this.callForm.Joken.DateSpecify = int.Parse(this.callForm.txtDateSpecify.Text);
                    this.callForm.Joken.DateSpecify2 = int.Parse(this.callForm.txtDateSpecify2.Text);
                    this.callForm.Joken.DateFrom =
                        string.IsNullOrWhiteSpace(this.callForm.dtpDateFrom.Text) ?
                        string.Empty : string.Format("{0:yyyy/MM/dd}", ((DateTime)this.callForm.dtpDateFrom.Value));
                    this.callForm.Joken.DateTo =
                        string.IsNullOrWhiteSpace(this.callForm.dtpDateTo.Text) ?
                        string.Empty : string.Format("{0:yyyy/MM/dd}", ((DateTime)this.callForm.dtpDateTo.Value));
                    this.callForm.Joken.KyotenCd = this.callForm.txtKyotenCd.Text;
                    this.callForm.Joken.TorihikisakiCdFrom = this.callForm.txtTorihikisakiCdFrom.Text;
                    this.callForm.Joken.TorihikisakiCdTo = this.callForm.txtTorihikisakiCdTo.Text;
                    this.callForm.Joken.GyoushaCdFrom = this.callForm.txtGyoushaCdFrom.Text;
                    this.callForm.Joken.GyoushaCdTo = this.callForm.txtGyoushaCdTo.Text;
                    this.callForm.Joken.GenbaCdFrom = this.callForm.txtGenbaCdFrom.Text;
                    this.callForm.Joken.GenbaCdTo = this.callForm.txtGenbaCdTo.Text;
                    this.callForm.Joken.NyuukinsakiCdFrom = this.callForm.txtNyuukinsakiCdFrom.Text;
                    this.callForm.Joken.NyuukinsakiCdTo = this.callForm.txtNyuukinsakiCdTo.Text;
                    this.callForm.Joken.BankCdFrom = this.callForm.txtBankCdFrom.Text;
                    this.callForm.Joken.BankCdTo = this.callForm.txtBankCdTo.Text;
                    this.callForm.Joken.BankShitenCdFrom = this.callForm.txtBankShitenCdFrom.Text;
                    this.callForm.Joken.BankShitenCdTo = this.callForm.txtBankShitenCdTo.Text;

                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("JokenSave", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        internal void FormClose()
        {
            LogUtility.DebugMethodStart();
            this.callForm.Close();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        internal void InputToCopy(TextBox inputTo)
        {
            var inputPrefixIdx = inputTo.Name.LastIndexOf("To");
            if (inputPrefixIdx < 0)
            {
                inputPrefixIdx = 0;
            }
            var inputPrefix = inputTo.Name.Substring(0, inputPrefixIdx);

            var namePrefixIdx = inputPrefix.LastIndexOf("Cd");
            if (namePrefixIdx < 0)
            {
                namePrefixIdx = 0;
            }
            var namePrefix = inputPrefix.Substring(0, namePrefixIdx);

            var inputToName = inputTo.Name;
            var nameToName = namePrefix + "NmTo";
            var inputFromName = inputPrefix + "From";
            var nameFromName = namePrefix + "NmFrom";

            var ctrlUtil = new ControlUtility();
            ctrlUtil.ControlCollection = this.callForm.Controls;

            var nameTo = ctrlUtil.GetSettingField(nameToName) as TextBox;
            var inputFrom = ctrlUtil.GetSettingField(inputFromName) as TextBox;
            var nameFrom = ctrlUtil.GetSettingField(nameFromName) as TextBox;

            if (inputFrom != null)
                inputTo.Text = inputFrom.Text;
            if (nameFrom != null && nameTo != null)
                nameTo.Text = nameFrom.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txtGenbaCd"></param>
        /// <returns></returns>
        internal bool GenbaCheckAndSetting(CustomTextBox txtGenbaCd, bool isInit = false)
        {
            LogUtility.DebugMethodStart(txtGenbaCd, isInit);
            bool ret = true;

            var txtNameSuffix = txtGenbaCd.Name.Substring(10); // "txtGenbaCd".Length
            var txtGenbaCdName = txtGenbaCd.Name;
            var txtGenbaNmName = "txtGenbaNm" + txtNameSuffix;
            var txtGyoushaCdName = "txtGyoushaCd" + txtNameSuffix;

            try
            {
                var ctrlUtil = new ControlUtility();
                ctrlUtil.ControlCollection = this.callForm.Controls;

                var txtGenbaNm = ctrlUtil.GetSettingField(txtGenbaNmName) as CustomTextBox;
                var txtGyoushaCd = ctrlUtil.GetSettingField(txtGyoushaCdName) as CustomTextBox;

                if (txtGyoushaCd == null || string.IsNullOrWhiteSpace(txtGyoushaCd.Text))
                {
                    txtGenbaCd.Text = string.Empty;
                    txtGenbaNm.Text = string.Empty;
                    ret = false;
                }
                else if (string.IsNullOrWhiteSpace(txtGenbaCd.Text))
                {
                    txtGenbaNm.Text = string.Empty;
                    ret = true;
                }
                else
                {
                    var mGenba = this.dbAccessor.GetGenba(txtGyoushaCd.Text, txtGenbaCd.Text, bool.Parse(this.callForm.ISNOT_NEED_DELETE_FLG.Text));
                    if (mGenba == null)
                    {
                        if (!isInit)
                            this.msgLogic.MessageBoxShow("E020", "現場");
                        txtGenbaNm.Text = string.Empty;
                        ret = false;
                    }
                    else
                    {
                        txtGenbaNm.Text = mGenba.GENBA_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException sqlex)
            {
                LogUtility.Error("GenbaCheckAndSetting", sqlex);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaCheckAndSetting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txtBankShitenCd"></param>
        /// <returns></returns>
        internal bool BankShitenCdCheckAndSetting(CustomTextBox txtBankShitenCd, bool isInit = false)
        {
            LogUtility.DebugMethodStart(txtBankShitenCd, isInit);
            bool ret = true;

            var txtNameSuffix = txtBankShitenCd.Name.Substring(15); // "txtBankShitenCd".Length
            var txtBankShitenCdName = txtBankShitenCd.Name;
            var txtBankShitenNmName = "txtBankShitenNm" + txtNameSuffix;
            var txtBankCdName = "txtBankCd" + txtNameSuffix;

            try
            {
                var ctrlUtil = new ControlUtility();
                ctrlUtil.ControlCollection = this.callForm.Controls;

                var txtBankShitenNm = ctrlUtil.GetSettingField(txtBankShitenNmName) as CustomTextBox;
                var txtBankCd = ctrlUtil.GetSettingField(txtBankCdName) as CustomTextBox;

                if (txtBankCd == null || string.IsNullOrWhiteSpace(txtBankCd.Text))
                {
                    txtBankShitenCd.Text = string.Empty;
                    txtBankShitenNm.Text = string.Empty;
                    ret = false;
                }
                else if (string.IsNullOrWhiteSpace(txtBankShitenCd.Text))
                {
                    txtBankShitenNm.Text = string.Empty;
                    ret = true;
                }
                else
                {
                    var mBankShiten = this.dbAccessor.GetBankShiten(txtBankCd.Text, txtBankShitenCd.Text, bool.Parse(this.callForm.ISNOT_NEED_DELETE_FLG.Text));
                    if (mBankShiten == null)
                    {
                        if (!isInit)
                            this.msgLogic.MessageBoxShow("E020", "銀行支店");
                        txtBankShitenNm.Text = string.Empty;
                        ret = false;
                    }
                    else
                    {
                        txtBankShitenNm.Text = mBankShiten.BANK_SHIETN_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException sqlex)
            {
                LogUtility.Error("BankShitenCheckAndSetting", sqlex);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BankShitenCheckAndSetting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region インタフェース実装

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>未実装</remarks>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <remarks>未実装</remarks>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <remarks>未実装</remarks>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>未実装</remarks>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>未実装</remarks>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}