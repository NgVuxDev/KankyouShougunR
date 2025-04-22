using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor;
using Shougun.Core.Common.HanyoCSVShutsuryoku.APP;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Const;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Utility;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Logic
{
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        private UIForm callForm;

        /// <summary>
        ///
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        ///
        /// </summary>
        private MasterDbAccessor dbAccessor;

        /// <summary>
        /// DBアクセス
        /// </summary>
        private PatternDbAccessor patternDbAccessor;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm callForm)
        {
            LogUtility.DebugMethodStart(callForm);

            this.callForm = callForm;

            this.dbAccessor = new MasterDbAccessor();
            this.patternDbAccessor = new PatternDbAccessor();

            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                this.parentForm = this.callForm.Parent as BusinessBaseForm;

                // ボタン初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();

                // プロパティから前回設定を呼び込む
                this.SettingsLoad();
                this.JokenDispTextRefresh();
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
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            ButtonControlUtility.SetButtonInfo(
                new ButtonSetting().LoadButtonSetting(Assembly.GetExecutingAssembly(), UIConstants.ButtonInfoXmlPath),
                this.parentForm, this.callForm.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            #region 既存イベント解除

            this.parentForm.bt_func1.Click -= this.callForm.bt_func1_Click;
            this.parentForm.bt_func2.Click -= this.callForm.bt_func2_Click;
            this.parentForm.bt_func3.Click -= this.callForm.bt_func3_Click;
            this.parentForm.bt_func4.Click -= this.callForm.bt_func4_Click;
            this.parentForm.bt_func6.Click -= this.callForm.bt_func6_Click;
            this.parentForm.bt_func8.Click -= this.callForm.bt_func8_Click;
            this.parentForm.bt_func12.Click -= this.callForm.bt_func12_Click;

            #endregion

            #region 新しいイベントバインド

            this.parentForm.bt_func1.Click += this.callForm.bt_func1_Click;
            this.parentForm.bt_func2.Click += this.callForm.bt_func2_Click;
            this.parentForm.bt_func3.Click += this.callForm.bt_func3_Click;
            this.parentForm.bt_func4.Click += this.callForm.bt_func4_Click;
            this.parentForm.bt_func6.Click += this.callForm.bt_func6_Click;
            this.parentForm.bt_func8.Click += this.callForm.bt_func8_Click;
            this.parentForm.bt_func12.Click += this.callForm.bt_func12_Click;

            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doBind"></param>
        /// <returns></returns>
        internal bool ParentShownBind(bool doBind)
        {
            LogUtility.DebugMethodStart(doBind);
            bool ret = true;

            try
            {
                // 既存イベント解除
                this.parentForm.Shown -= this.callForm.parentForm_Shown;

                // 新しいイベントバインド
                if (doBind)
                    this.parentForm.Shown += this.callForm.parentForm_Shown;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ParentShownBind", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <remarks>F12実処理</remarks>
        internal void FormClose()
        {
            LogUtility.DebugMethodStart();

            this.callForm.Close();
            this.parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// F6実処理
        /// 選択しているパターンで、
        /// 各業務テーブルからデータを取得し、CSVファイルに出力
        /// </remarks>
        internal bool CsvOutput()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (ret)
                    if (this.callForm.dgvPatterns.CurrentCell == null ||
                        this.callForm.dgvPatterns.CurrentCell.RowIndex < 0)
                    {
                        this.msgLogic.MessageBoxShow("E051", "パターン");
                        ret = false;
                    }

                if (ret)
                {
                    var curDgvRowIndex = this.callForm.dgvPatterns.CurrentCell.RowIndex;
                    var dtPatterns = this.callForm.dgvPatterns.DataSource as DataTable;
                    if (dtPatterns != null && dtPatterns.Rows.Count > 0)
                    {
                        var curDataRow = dtPatterns.Rows[curDgvRowIndex];
                        var curPattern = this.callForm.Patterns.FirstOrDefault(
                            x => x.SYSTEM_ID.Equals(curDataRow["SYSTEM_ID"]) && x.SEQ.Equals(curDataRow["SEQ"])
                            );
                        if (curPattern != null)
                            new CsvOutputUtility(this.callForm, this.callForm.Joken, this.FullPatternCreate(curPattern)).CsvOutput();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CsvOutput", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mCsvPattern"></param>
        /// <returns></returns>
        private PatternDto FullPatternCreate(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            var pattern = new PatternDto(this.callForm.Joken.HaniKbn);

            pattern.CsvPattern = this.patternDbAccessor.GetCsvPattern(mCsvPattern);
            pattern.CsvPatternColumns = this.patternDbAccessor.GetCsvPatternColumns(mCsvPattern);
            pattern.CsvPatternHanbaikanri = this.patternDbAccessor.GetCsvPatternHanbaikanri(mCsvPattern);
            pattern.CsvPatternNyuushukkin = this.patternDbAccessor.GetCsvPatternNyuushukkin(mCsvPattern);

            return pattern;
        }

        /// <summary>
        /// 出力条件指定画面を呼び出す
        /// </summary>
        /// <remarks>F1実処理</remarks>
        internal bool JokenLoad()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                if (this.callForm.Joken == null)
                    this.SettingsLoad();

                var jokenPopupForm = new JokenForm(this.callForm.Joken);
                jokenPopupForm.sysDate = this.parentForm.sysDate;
                switch (jokenPopupForm.ShowDialog())
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        this.SettingsSave(jokenPopupForm.Joken);
                        this.JokenDispTextRefresh();
                        this.PatternsClear();
                        ret = true;
                        break;

                    default:
                        ret = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("JokenLoad", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        internal void PatternsClear()
        {
            this.callForm.txtCondition.Text = string.Empty;

            if (this.callForm.Patterns != null)
                this.callForm.Patterns.Clear();

            var dtPatterns = this.callForm.dgvPatterns.DataSource as DataTable;
            if (dtPatterns != null)
                dtPatterns.Rows.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        private void SettingsLoad()
        {
            var joken = new JokenDto();

            #region プロパティ読込む

            // 出力範囲
            // 1. 販売管理、2. 入金・出金
            joken.HaniKbn = Properties.Settings.Default.HaniKbn;
            if (joken.HaniKbn < 1 || joken.HaniKbn > 2) // 範囲外の場合、最小値に設定
                joken.HaniKbn = 1;

            // 日付
            // 1. 伝票日付、2. 売上日付、3. 支払日付、4. 入力日付
            joken.DateSpecify = Properties.Settings.Default.DateSpecify;
            if (joken.DateSpecify < 1 || joken.DateSpecify > 4)
                joken.DateSpecify = 1;

            // 日付範囲
            // 1. 当日、2. 当月、3. 期間指定
            joken.DateSpecify2 = Properties.Settings.Default.DateSpecify2;
            if (joken.DateSpecify2 < 1 || joken.DateSpecify2 > 3)
                joken.DateSpecify2 = 1;

            // 開始終了日
            switch (joken.DateSpecify2)
            {
                case 1:
                    joken.DateFrom = string.Format("{0:yyyy/MM/dd}", this.parentForm.sysDate);
                    joken.DateTo = string.Format("{0:yyyy/MM/dd}", this.parentForm.sysDate);
                    break;

                case 2:
                    var sysDate = this.parentForm.sysDate;
                    joken.DateFrom = string.Format("{0:yyyy/MM/dd}", new DateTime(sysDate.Year, sysDate.Month, 1));
                    joken.DateTo = string.Format("{0:yyyy/MM/dd}", new DateTime(sysDate.Year, sysDate.Month, 1).AddMonths(1).AddDays(-1));
                    break;

                case 3:
                    joken.DateFrom = Properties.Settings.Default.DateFrom;
                    joken.DateTo = Properties.Settings.Default.DateTo;
                    break;

                default:
                    break;
            }

            // 拠点
            joken.KyotenCd = Properties.Settings.Default.KyotenCd;
            if (string.IsNullOrWhiteSpace(joken.KyotenCd))
                //前回保存値がない場合、99で設定
                joken.KyotenCd = "99";

            // 開始終了取引先
            joken.TorihikisakiCdFrom = Properties.Settings.Default.TorihikisakiCdFrom;
            joken.TorihikisakiCdTo = Properties.Settings.Default.TorihikisakiCdTo;

            // 開始終了業者
            joken.GyoushaCdFrom = Properties.Settings.Default.GyoushaCdFrom;
            joken.GyoushaCdTo = Properties.Settings.Default.GyoushaCdTo;

            // 開始終了現場
            joken.GenbaCdFrom = Properties.Settings.Default.GenbaCdFrom;
            joken.GenbaCdTo = Properties.Settings.Default.GenbaCdTo;

            // 開始終了入金先
            joken.NyuukinsakiCdFrom = Properties.Settings.Default.NyuukinsakiCdFrom;
            joken.NyuukinsakiCdTo = Properties.Settings.Default.NyuukinsakiCdTo;

            // 開始終了銀行
            joken.BankCdFrom = Properties.Settings.Default.BankCdFrom;
            joken.BankCdTo = Properties.Settings.Default.BankCdTo;

            // 開始終了銀行支店
            joken.BankShitenCdFrom = Properties.Settings.Default.BankShitenCdFrom;
            joken.BankShitenCdTo = Properties.Settings.Default.BankShitenCdTo;

            #endregion

            this.callForm.Joken = joken;
        }

        /// <summary>
        ///
        /// </summary>
        private void SettingsSave(JokenDto joken)
        {
            #region プロパティ保存

            // 出力範囲
            Properties.Settings.Default.HaniKbn = joken.HaniKbn;

            // 日付
            Properties.Settings.Default.DateSpecify = joken.DateSpecify;

            // 日付範囲
            Properties.Settings.Default.DateSpecify2 = joken.DateSpecify2;

            // 開始終了日
            if (joken.DateSpecify2 == 3)
            {
                Properties.Settings.Default.DateFrom = joken.DateFrom;
                Properties.Settings.Default.DateTo = joken.DateTo;
            }

            // 拠点
            Properties.Settings.Default.KyotenCd = joken.KyotenCd;

            // 開始終了取引先
            Properties.Settings.Default.TorihikisakiCdFrom = joken.TorihikisakiCdFrom;
            Properties.Settings.Default.TorihikisakiCdTo = joken.TorihikisakiCdTo;

            // 開始終了業者
            Properties.Settings.Default.GyoushaCdFrom = joken.GyoushaCdFrom;
            Properties.Settings.Default.GyoushaCdTo = joken.GyoushaCdTo;

            // 開始終了現場
            Properties.Settings.Default.GenbaCdFrom = joken.GenbaCdFrom;
            Properties.Settings.Default.GenbaCdTo = joken.GenbaCdTo;

            // 開始終了入金先
            Properties.Settings.Default.NyuukinsakiCdFrom = joken.NyuukinsakiCdFrom;
            Properties.Settings.Default.NyuukinsakiCdTo = joken.NyuukinsakiCdTo;

            // 開始終了銀行
            Properties.Settings.Default.BankCdFrom = joken.BankCdFrom;
            Properties.Settings.Default.BankCdTo = joken.BankCdTo;

            // 開始終了銀行支店
            Properties.Settings.Default.BankShitenCdFrom = joken.BankShitenCdFrom;
            Properties.Settings.Default.BankShitenCdTo = joken.BankShitenCdTo;

            #endregion

            this.callForm.Joken = joken;
        }

        /// <summary>
        ///
        /// </summary>
        private void JokenDispTextRefresh()
        {
            LogUtility.DebugMethodStart();

            var joken = this.callForm.Joken;
            var jokenDispText = new StringBuilder();
            // 出力範囲は1又は2の場合のみ出力する
            if (joken.HaniKbn == 1 || joken.HaniKbn == 2)
            {
                // タイトル
                jokenDispText.AppendLine("【出力条件】");

                // 出力範囲
                jokenDispText.AppendLine(
                    "［出力範囲］" +
                    UIConstants.OUTPUT_HANI_KBNS.SingleOrDefault(x => x.Item1 == joken.HaniKbn).Item2 +
                    UIConstants.OUTPUT_HANI_KBNS.SingleOrDefault(x => x.Item1 == joken.HaniKbn).Item3);

                // 日付
                var dateDummyFrom = DateTime.Now;
                var dateDummyTo = DateTime.Now;
                var dateFrom = DateTime.TryParse(joken.DateFrom, out dateDummyFrom) ? dateDummyFrom.ToString("yyyy/MM/dd(ddd)") : string.Empty;
                var dateTo = DateTime.TryParse(joken.DateTo, out dateDummyTo) ? dateDummyTo.ToString("yyyy/MM/dd(ddd)") : string.Empty;
                jokenDispText.AppendLine(
                    "［" + UIConstants.DATE_SPECIFY_KBNS.SingleOrDefault(x => x.Item1 == joken.DateSpecify).Item2 + "］" +
                    dateFrom + " ～ " + dateTo);

                // 拠点
                var kyotenCd = (short)-1;
                var kyotenNm = string.Empty;
                if (!string.IsNullOrWhiteSpace(joken.KyotenCd) && short.TryParse(joken.KyotenCd, out kyotenCd))
                {
                    var kyoten = dbAccessor.GetKyoten(kyotenCd);
                    if (kyoten != null && !kyoten.KYOTEN_CD.IsNull)
                        kyotenNm = kyoten.KYOTEN_NAME_RYAKU;
                }
                jokenDispText.AppendLine("［拠点］" + kyotenNm);

                // 取引先
                if (string.IsNullOrWhiteSpace(joken.TorihikisakiCdFrom) && string.IsNullOrWhiteSpace(joken.TorihikisakiCdTo))
                {
                    jokenDispText.AppendLine("［取引先］全て");
                }
                else
                {
                    var torihikisakiNmFrom = string.Empty;
                    if (!string.IsNullOrWhiteSpace(joken.TorihikisakiCdFrom))
                    {
                        var torihikisaki = dbAccessor.GetTorihikisaki(joken.TorihikisakiCdFrom);
                        if (torihikisaki != null && !string.IsNullOrWhiteSpace(torihikisaki.TORIHIKISAKI_CD))
                            torihikisakiNmFrom = string.Format("{0} {1}", torihikisaki.TORIHIKISAKI_CD, torihikisaki.TORIHIKISAKI_NAME_RYAKU);
                    }
                    var torihikisakiNmTo = string.Empty;
                    if (!string.IsNullOrWhiteSpace(joken.TorihikisakiCdTo))
                    {
                        var torihikisaki = dbAccessor.GetTorihikisaki(joken.TorihikisakiCdTo);
                        if (torihikisaki != null && !string.IsNullOrWhiteSpace(torihikisaki.TORIHIKISAKI_CD))
                            torihikisakiNmTo = string.Format("{0} {1}", torihikisaki.TORIHIKISAKI_CD, torihikisaki.TORIHIKISAKI_NAME_RYAKU);
                    }
                    jokenDispText.AppendLine("［取引先］" + torihikisakiNmFrom + " ～ " + torihikisakiNmTo);
                }

                // 出力範囲により分岐処理
                switch (joken.HaniKbn)
                {
                    case 1:
                        // 業者
                        if (string.IsNullOrWhiteSpace(joken.GyoushaCdFrom) && string.IsNullOrWhiteSpace(joken.GyoushaCdTo))
                        {
                            jokenDispText.AppendLine("［業者］全て");
                        }
                        else
                        {
                            var gyoushaNmFrom = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.GyoushaCdFrom))
                            {
                                var gyousha = dbAccessor.GetGyousha(joken.GyoushaCdFrom);
                                if (gyousha != null && !string.IsNullOrWhiteSpace(gyousha.GYOUSHA_CD))
                                    gyoushaNmFrom = string.Format("{0} {1}", gyousha.GYOUSHA_CD, gyousha.GYOUSHA_NAME_RYAKU);
                            }
                            var gyoushaNmTo = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.GyoushaCdTo))
                            {
                                var gyousha = dbAccessor.GetGyousha(joken.GyoushaCdTo);
                                if (gyousha != null && !string.IsNullOrWhiteSpace(gyousha.GYOUSHA_CD))
                                    gyoushaNmTo = string.Format("{0} {1}", gyousha.GYOUSHA_CD, gyousha.GYOUSHA_NAME_RYAKU);
                            }
                            jokenDispText.AppendLine("［業者］" + gyoushaNmFrom + " ～ " + gyoushaNmTo);
                        }

                        // 現場
                        if (string.IsNullOrWhiteSpace(joken.GenbaCdFrom) && string.IsNullOrWhiteSpace(joken.GenbaCdTo))
                        {
                            jokenDispText.AppendLine("［現場］全て");
                        }
                        else
                        {
                            var genbaNmFrom = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.GenbaCdFrom))
                            {
                                var genba = dbAccessor.GetGenba(joken.GyoushaCdFrom, joken.GenbaCdFrom);
                                if (genba != null && !string.IsNullOrWhiteSpace(genba.GENBA_CD))
                                    genbaNmFrom = string.Format("{0} {1}", genba.GENBA_CD, genba.GENBA_NAME_RYAKU);
                            }
                            var genbaNmTo = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.GenbaCdTo))
                            {
                                var genba = dbAccessor.GetGenba(joken.GyoushaCdTo, joken.GenbaCdTo);
                                if (genba != null && !string.IsNullOrWhiteSpace(genba.GENBA_CD))
                                    genbaNmTo = string.Format("{0} {1}", genba.GENBA_CD, genba.GENBA_NAME_RYAKU);
                            }
                            jokenDispText.AppendLine("［現場］" + genbaNmFrom + " ～ " + genbaNmTo);
                        }
                        break;

                    case 2:
                        // 入金先
                        if (string.IsNullOrWhiteSpace(joken.NyuukinsakiCdFrom) && string.IsNullOrWhiteSpace(joken.NyuukinsakiCdTo))
                        {
                            jokenDispText.AppendLine("［入金先］全て");
                        }
                        else
                        {
                            var nyuukinsakiNmFrom = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.NyuukinsakiCdFrom))
                            {
                                var nyuukinsaki = dbAccessor.GetNyuukinsaki(joken.NyuukinsakiCdFrom);
                                if (nyuukinsaki != null && !string.IsNullOrWhiteSpace(nyuukinsaki.NYUUKINSAKI_CD))
                                    nyuukinsakiNmFrom = string.Format("{0} {1}", nyuukinsaki.NYUUKINSAKI_CD, nyuukinsaki.NYUUKINSAKI_NAME_RYAKU);
                            }
                            var nyuukinsakiNmTo = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.NyuukinsakiCdTo))
                            {
                                var nyuukinsaki = dbAccessor.GetNyuukinsaki(joken.NyuukinsakiCdTo);
                                if (nyuukinsaki != null && !string.IsNullOrWhiteSpace(nyuukinsaki.NYUUKINSAKI_CD))
                                    nyuukinsakiNmTo = string.Format("{0} {1}", nyuukinsaki.NYUUKINSAKI_CD, nyuukinsaki.NYUUKINSAKI_NAME_RYAKU);
                            }
                            jokenDispText.AppendLine("［入金先］" + nyuukinsakiNmFrom + " ～ " + nyuukinsakiNmTo);
                        }

                        // 銀行
                        if (string.IsNullOrWhiteSpace(joken.BankCdFrom) && string.IsNullOrWhiteSpace(joken.BankCdTo))
                        {
                            jokenDispText.AppendLine("［銀行］全て");
                        }
                        else
                        {
                            var bankNmFrom = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.BankCdFrom))
                            {
                                var bank = dbAccessor.GetBank(joken.BankCdFrom);
                                if (bank != null && !string.IsNullOrWhiteSpace(bank.BANK_CD))
                                    bankNmFrom = string.Format("{0} {1}", bank.BANK_CD, bank.BANK_NAME_RYAKU);
                            }
                            var bankNmTo = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.BankCdTo))
                            {
                                var bank = dbAccessor.GetBank(joken.BankCdTo);
                                if (bank != null && !string.IsNullOrWhiteSpace(bank.BANK_CD))
                                    bankNmTo = string.Format("{0} {1}", bank.BANK_CD, bank.BANK_NAME_RYAKU);
                            }
                            jokenDispText.AppendLine("［銀行］" + bankNmFrom + " ～ " + bankNmTo);
                        }

                        // 銀行支店
                        if (string.IsNullOrWhiteSpace(joken.BankShitenCdFrom) && string.IsNullOrWhiteSpace(joken.BankShitenCdTo))
                        {
                            jokenDispText.AppendLine("［銀行支店］全て");
                        }
                        else
                        {
                            var bankShitenNmFrom = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.BankShitenCdFrom))
                            {
                                var bankShiten = dbAccessor.GetBankShiten(joken.BankCdFrom, joken.BankShitenCdFrom);
                                if (bankShiten != null && !string.IsNullOrWhiteSpace(bankShiten.BANK_SHITEN_CD))
                                    bankShitenNmFrom = string.Format("{0} {1}", bankShiten.BANK_SHITEN_CD, bankShiten.BANK_SHIETN_NAME_RYAKU);
                            }
                            var bankShitenNmTo = string.Empty;
                            if (!string.IsNullOrWhiteSpace(joken.BankShitenCdTo))
                            {
                                var bankShiten = dbAccessor.GetBankShiten(joken.BankCdTo, joken.BankShitenCdTo);
                                if (bankShiten != null && !string.IsNullOrWhiteSpace(bankShiten.BANK_SHITEN_CD))
                                    bankShitenNmTo = string.Format("{0} {1}", bankShiten.BANK_SHITEN_CD, bankShiten.BANK_SHIETN_NAME_RYAKU);
                            }
                            jokenDispText.AppendLine("［銀行支店］" + bankShitenNmFrom + " ～ " + bankShitenNmTo);
                        }
                        break;

                    default:
                        break;
                }
            }
            this.callForm.txtJokenDisp.Text = jokenDispText.ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="windowType"></param>
        /// <remarks>F2、F3、F4実処理</remarks>
        internal bool PatternLoad(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            var joken = this.callForm.Joken;
            var pattern = new PatternDto(this.callForm.Joken.HaniKbn);
            var dtPatterns = this.callForm.dgvPatterns.DataSource as DataTable;
            switch (windowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // ret = true;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    if (ret)
                        if (this.callForm.dgvPatterns.CurrentCell == null ||
                            this.callForm.dgvPatterns.CurrentCell.RowIndex < 0)
                        {
                            this.msgLogic.MessageBoxShow("E051", "パターン");
                            ret = false;
                        }

                    if (ret)
                    {
                        // 一旦False
                        ret = false;

                        // パターン検索
                        var curDgvRowIndex = this.callForm.dgvPatterns.CurrentCell.RowIndex;
                        if (dtPatterns != null && dtPatterns.Rows.Count > 0)
                        {
                            var curDataRow = dtPatterns.Rows[curDgvRowIndex];
                            var curPattern = this.callForm.Patterns.FirstOrDefault(
                                x => x.SYSTEM_ID.Equals(curDataRow["SYSTEM_ID"]) && x.SEQ.Equals(curDataRow["SEQ"])
                                );
                            if (curPattern != null)
                            {
                                // パターン取得した後Trueに
                                pattern.CsvPattern = curPattern;
                                ret = true;
                            }
                        }
                    }
                    break;

                default:
                    ret = false;
                    break;
            }

            if (ret)
            {
                var patternHeaderForm = new HeaderBaseForm();
                var patternCallForm = new PatternForm(joken, pattern, windowType);
                var patternPopupForm = new BasePopForm(patternCallForm, patternHeaderForm);
                if (!new FormControlLogic().ScreenPresenceCheck(patternCallForm))
                    patternPopupForm.ShowDialog();
            }

            if (dtPatterns != null && dtPatterns.Rows.Count > 0)
            {
                // 既に検索が掛かってるので、再検索する
                this.Search();
                this.DispSourceRefresh();
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool DispSourceRefresh(bool afterPatternLoad = false)
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            var dtPatterns = this.callForm.dgvPatterns.DataSource as DataTable;
            try
            {
                if (dtPatterns == null)
                {
                    dtPatterns = new DataTable();
                    dtPatterns.Columns.Add("SYSTEM_ID", typeof(SqlInt64));
                    dtPatterns.Columns.Add("SEQ", typeof(SqlInt32));
                    dtPatterns.Columns.Add("OUTPUT_KBN_NAME", typeof(string));
                    dtPatterns.Columns.Add("PATTERN_NAME", typeof(string));
                    dtPatterns.Columns.Add("PATTERN_BIKOU", typeof(string));
                }
                else
                {
                    dtPatterns.Rows.Clear();
                }

                if (this.callForm.Patterns != null)
                {
                    var lstPatterns = this.callForm.Patterns;
                    if (!string.IsNullOrWhiteSpace(this.callForm.txtCondition.Text))
                        lstPatterns = this.callForm.Patterns.Where(p => p.PATTERN_NAME.Contains(this.callForm.txtCondition.Text.Trim())).ToList();

                    if (lstPatterns != null || lstPatterns.Count > 0)
                    {
                        foreach (var pattern in lstPatterns)
                        {
                            var row = dtPatterns.NewRow();

                            row["SYSTEM_ID"] = pattern.SYSTEM_ID;
                            row["SEQ"] = pattern.SEQ;
                            row["PATTERN_NAME"] = pattern.PATTERN_NAME;
                            row["PATTERN_BIKOU"] = pattern.BIKOU;

                            row["OUTPUT_KBN_NAME"] = string.Empty;
                            if (pattern.OUTPUT_KBN == 1 || pattern.OUTPUT_KBN == 2)
                                row["OUTPUT_KBN_NAME"] = UIConstants.OUTPUT_KBNS[pattern.OUTPUT_KBN.Value - 1];

                            dtPatterns.Rows.Add(row);
                        }
                    }
                }

                // パターン変更後ではない場合
                if (!afterPatternLoad)
                    if (dtPatterns.Rows.Count == 0)
                        this.msgLogic.MessageBoxShow("E076"); // データが存在しない(但しデータバインドは通常に行う)
            }
            catch (Exception ex)
            {
                LogUtility.Error("DispSourceRefresh", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }
            finally
            {
                this.callForm.dgvPatterns.DataSource = dtPatterns;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region インタフェース実装

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int ret = 0;

            try
            {
                if (this.callForm.Patterns == null)
                    this.callForm.Patterns = new List<M_OUTPUT_CSV_PATTERN>();
                else
                    this.callForm.Patterns.Clear();

                var patterns = this.patternDbAccessor.GetCsvPatterns(this.callForm.Joken.HaniKbn);
                if (patterns != null)
                {
                    this.callForm.Patterns.AddRange(patterns);
                    ret = patterns.Count;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PanelSearch", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}