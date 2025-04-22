using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Scale.KeiryouHoukoku.DTO;
using Shougun.Core.Scale.KeiryouHoukoku.Logic;

namespace Shougun.Core.Scale.KeiryouHoukoku.APP
{
    /// <summary>
    /// 計量報告出力指定画面を表すクラス・コントロール
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region - Fields -

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        ///
        /// </summary>
        private MessageBoxShowLogic msglogic;
        internal MessageBoxShowLogic MsgLogic { get { return this.msglogic; } }

        /// <summary>
        /// 共通情報(SysInfoなど)
        /// </summary>
        internal CommonInformation CommInfo { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal BusinessBaseForm BaseForm { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal HeaderBaseForm HeaderForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UIForm" /> class.</summary>
        /// <param name="windowId">ウィンドウＩＤ</param>
        public UIForm(WINDOW_ID windowId)
        {
            LogUtility.DebugMethodStart(windowId);

            this.InitializeComponent();

            this.WindowId = windowId;

            // メッセージロジック
            this.msglogic = new MessageBoxShowLogic();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool Initialize()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                // 報告種類
                this.HOUKOKU_SHURUI.Text = "1";
                this.logic.ChangeHoukokuShuruiState();
                // 拠点CD
                this.KYOTEN_CD.Text = "99";
                // 拠点
                this.KYOTEN_NAME_RYAKU.Text = "全社";
                this.DENPYOU_KBN_CD.Text = "1";
                // 日付CD
                this.HIDUKE_KBN.Text = "1";
                // 日付範囲
                this.HIDUKE_HANI.Text = "1";
                // 日付From
                this.HIDUKE_FROM.Text = String.Empty;
                // 日付To
                this.HIDUKE_TO.Text = String.Empty;
                // 計量区分
                this.KEIRYOU_KBN.Text = "3";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Initialize", ex);
                this.msglogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.BaseForm = this.Parent as BusinessBaseForm;
            this.HeaderForm = this.BaseForm.headerForm as HeaderBaseForm;
            this.CommInfo = (this.BaseForm.ribbonForm as RibbonMainMenu).GlobalCommonInformation;

            if (!this.logic.WindowInit())
                return;

            // 初期化処理
            this.Initialize();

            if (!this.isShown)
            {
                this.Height -= 7;
                this.isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 期間指定ラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void RadioButtonHidukeHaniShitei_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_FROM.Text = String.Empty;
            this.HIDUKE_TO.Text = String.Empty;

            if (this.rbtKikan.Checked)
            {
                // 日付範囲指定テキストボックスに（登録時）必須チェックを追加する
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                this.customPanelCond_4.Enabled = true;
                if (this.rbtSuiihyo.Checked)
                {
                    var date = this.BaseForm.sysDate;
                    this.HIDUKE_FROM.Value = new DateTime(date.Year, date.Month, 1);
                    this.HIDUKE_TO.Value = new DateTime(date.Year, date.Month, 1).AddYears(1).AddDays(-1);
                }
            }
            else
            {
                // 日付範囲指定テキストボックスの必須チェックをはずす
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>();
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>();

                this.customPanelCond_4.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckTorihikisakiCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                var cdTo = this.TORIHIKISAKI_CD_TO.Text;
                if (!string.IsNullOrWhiteSpace(cdFrom) && !string.IsNullOrWhiteSpace(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.TORIHIKISAKI_CD_FROM.IsInputErrorOccured = true;
                        this.TORIHIKISAKI_CD_TO.IsInputErrorOccured = true;

                        this.TORIHIKISAKI_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiCdFromTo", ex);
                this.msglogic.MessageBoxShow("E245");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 業者CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGyoushaCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.GYOUSHA_CD_FROM.Text;
                var cdTo = this.GYOUSHA_CD_TO.Text;
                if (!string.IsNullOrWhiteSpace(cdFrom) && !string.IsNullOrWhiteSpace(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                        this.GYOUSHA_CD_TO.IsInputErrorOccured = true;

                        this.GYOUSHA_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCdFromTo", ex);
                this.msglogic.MessageBoxShow("E245");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 現場CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGenbaCdFromTo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = true;

            try
            {
                var cdFrom = this.GENBA_CD_FROM.Text;
                var cdTo = this.GENBA_CD_TO.Text;
                if (!string.IsNullOrWhiteSpace(cdFrom) && !string.IsNullOrWhiteSpace(cdTo))
                {
                    if (0 < cdFrom.CompareTo(cdTo))
                    {
                        this.GENBA_CD_FROM.IsInputErrorOccured = true;
                        this.GENBA_CD_TO.IsInputErrorOccured = true;

                        this.GENBA_CD_FROM.Focus();

                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenbaCdFromTo", ex);
                this.msglogic.MessageBoxShow("E245");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// パターンDTOに画面からデータをセットします
        /// </summary>
        private bool SetDtoData(DTOCls dto)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                dto.KyotenCd = int.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME_RYAKU.Text;
                if (!string.IsNullOrEmpty(this.GROUP_TANI.Text))
                {
                    dto.GroupTani = int.Parse(this.GROUP_TANI.Text);
                }
                dto.DenpyouKbnCd = int.Parse(this.DENPYOU_KBN_CD.Text);
                dto.HoukokuShurui = int.Parse(this.HOUKOKU_SHURUI.Text);
                dto.DateShuruiCd = int.Parse(this.HIDUKE_KBN.Text);
                dto.DateHani = int.Parse(this.HIDUKE_HANI.Text);
                DateTime sysDateTime = DateTime.Now;
                sysDateTime = this.BaseForm.sysDate;
                // 日付範囲の選択状態で日付条件を設定
                if (this.rbtToday.Checked)
                {
                    dto.DateFrom = sysDateTime.ToString("yyyy/MM/dd");
                    dto.DateTo = sysDateTime.ToString("yyyy/MM/dd");
                }
                else if (this.rbtMonth.Checked)
                {
                    dto.DateFrom = new DateTime(sysDateTime.Year, sysDateTime.Month, 1).ToString("yyyy/MM/dd");
                    dto.DateTo = new DateTime(sysDateTime.Year, sysDateTime.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"); 
                }
                else if (this.rbtKikan.Checked)
                {
                    dto.DateFrom = Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                    dto.DateTo = Convert.ToDateTime(this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
                }
                dto.KeiryouKbn = int.Parse(this.KEIRYOU_KBN.Text);
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.GyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                dto.GyoushaCdTo = this.GYOUSHA_CD_TO.Text;
                dto.GyoushaFrom = this.GYOUSHA_NAME_FROM.Text;
                dto.GyoushaTo = this.GYOUSHA_NAME_TO.Text;
                dto.UpnGyoushaCdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
                dto.UpnGyoushaCdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
                dto.UpnGyoushaFrom = this.UNPAN_GYOUSHA_NAME_FROM.Text;
                dto.UpnGyoushaTo = this.UNPAN_GYOUSHA_NAME_TO.Text;
                dto.HinmeiCdFrom = this.HINMEI_CD_FROM.Text;
                dto.HinmeiCdTo = this.HINMEI_CD_TO.Text;
                dto.HinmeiFrom = this.HINMEI_NAME_FROM.Text;
                dto.HinmeiTo = this.HINMEI_NAME_TO.Text;
                dto.ShuruiCdFrom = this.SHURUI_CD_FROM.Text;
                dto.ShuruiCdTo = this.SHURUI_CD_TO.Text;
                dto.ShuruiFrom = this.SHURUI_NAME_FROM.Text;
                dto.ShuruiTo = this.SHURUI_NAME_TO.Text;
                dto.BunruiCdFrom = this.BUNRUI_CD_FROM.Text;
                dto.BunruiCdTo = this.BUNRUI_CD_TO.Text;
                dto.BunruiFrom = this.BUNRUI_NAME_FROM.Text;
                dto.BunruiTo = this.BUNRUI_NAME_TO.Text;
                dto.KeitaiKbnCdFrom = this.KEITAI_KBN_CD_FROM.Text;
                dto.KeitaiKbnCdTo = this.KEITAI_KBN_CD_TO.Text;
                dto.KeitaiKbnFrom = this.KEITAI_KBN_NAME_FROM.Text;
                dto.KeitaiKbnTo = this.KEITAI_KBN_NAME_TO.Text;

                // 現場はテキストボックスが使用不可の場合、条件なしとする
                if (this.GENBA_CD_FROM.Enabled)
                {
                    dto.GenbaCdFrom = this.GENBA_CD_FROM.Text;
                    dto.GenbaCdTo = this.GENBA_CD_TO.Text;
                    dto.GenbaFrom = this.GENBA_NAME_FROM.Text;
                    dto.GenbaTo = this.GENBA_NAME_TO.Text;
                }

                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupGyousha = this.GROUP_GYOUSHA.Checked;
                dto.IsGroupGenba = this.GROUP_GENBA.Checked;
                dto.IsGroupDenpyouNumber = this.GROUP_KEIRYOU_NUMBER.Checked;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDtoData", ex);
                this.msglogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private void RecoveryFocusOutCheckMethod()
        {
            LogUtility.DebugMethodStart();

            this.HOUKOKU_SHURUI.CausesValidation = true;
            this.GROUP_TANI.CausesValidation = true;
            this.KYOTEN_CD.CausesValidation = true;
            this.DENPYOU_KBN_CD.CausesValidation = true;
            this.HIDUKE_KBN.CausesValidation = true;
            this.HIDUKE_HANI.CausesValidation = true;
            this.HIDUKE_FROM.CausesValidation = true;
            this.HIDUKE_TO.CausesValidation = true;
            this.KEIRYOU_KBN.CausesValidation = true;
            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            this.GYOUSHA_CD_FROM.CausesValidation = true;
            this.GYOUSHA_CD_TO.CausesValidation = true;
            this.GENBA_CD_FROM.CausesValidation = true;
            this.GENBA_CD_TO.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
            this.HINMEI_CD_FROM.CausesValidation = true;
            this.HINMEI_CD_TO.CausesValidation = true;
            this.SHURUI_CD_FROM.CausesValidation = true;
            this.SHURUI_CD_TO.CausesValidation = true;
            this.BUNRUI_CD_FROM.CausesValidation = true;
            this.BUNRUI_CD_TO.CausesValidation = true;
            this.KEITAI_KBN_CD_FROM.CausesValidation = true;
            this.KEITAI_KBN_CD_TO.CausesValidation = true;

            LogUtility.DebugMethodEnd();
        }
        #endregion - Methods -

        #region - イベント -
        #region - キー処理 -

        /// <summary>Ｆ5キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
                this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
                if (this.RegistErrorFlag)
                {
                    this.RecoveryFocusOutCheckMethod();
                    return;
                }
                if (this.logic.CheckDate())
                {
                    return;
                }
                var dto = new DTOCls();
                this.SetDtoData(dto);

                this.logic.CSVPrint(dto);
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>Ｆ７キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
                this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
                if (this.RegistErrorFlag)
                {
                    this.RecoveryFocusOutCheckMethod();
                    return;
                }
                if (this.logic.CheckDate())
                {
                    return;
                }
                var dto = new DTOCls();
                this.SetDtoData(dto);

                this.logic.Search(dto);
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion - キー処理 -

        #region HIDUKE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.HIDUKE_FROM;
            var ToTextBox = this.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region TORIHIKISAKI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.TORIHIKISAKI_CD_FROM;
            var ToTextBox = this.TORIHIKISAKI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.GYOUSHA_CD_FROM;
            var ToTextBox = this.GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.GENBA_CD_FROM;
            var ToTextBox = this.GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.UNPAN_GYOUSHA_CD_FROM;
            var ToTextBox = this.UNPAN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region HINMEI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HINMEI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.HINMEI_CD_FROM;
            var ToTextBox = this.HINMEI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHURUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.SHURUI_CD_FROM;
            var ToTextBox = this.SHURUI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region BUNRUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BUNRUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.BUNRUI_CD_FROM;
            var ToTextBox = this.BUNRUI_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region KEITAI_KBN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KEITAI_KBN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.KEITAI_KBN_CD_FROM;
            var ToTextBox = this.KEITAI_KBN_CD_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 業者CDFromテキストボックスのバリデーションが完了したときに処理します
        /// <summary>
        /// 業者CDFromテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ChangeGenbaState();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 業者CDToテキストボックスのバリデーションが完了したときに処理します
        /// <summary>
        /// 業者CDToFromテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GYOUSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ChangeGenbaState();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 排出事業者ポップアップから戻ってきたら実行されるイベント
        /// <summary>
        /// 排出事業者ポップアップから戻ってきたら実行されるイベント
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GYOUSHA_POPUPAFTEREXECUTE(ICustomControl icc, DialogResult drt)
        {
            LogUtility.DebugMethodStart(icc, drt);

            if (drt == DialogResult.OK)
            {
                this.logic.ChangeGenbaState();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 計量明細表指定ラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void RadioButtonMeisaihyo_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ChangeHoukokuShuruiState();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 現場CDFromのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.GYOUSHA_CD_FROM.Text;
            var genbaCd = this.GENBA_CD_FROM.Text;

            if (string.IsNullOrWhiteSpace(genbaCd))
            {
                this.GENBA_NAME_FROM.Text = String.Empty;
            }
            else if (!string.IsNullOrWhiteSpace(genbaCd))
            {
                if (!this.CheckGyoushaCd())
                {
                    e.Cancel = true;
                }
                else if (!this.CheckGenba(gyoushaCd, genbaCd, true))
                {
                    e.Cancel = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDToのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.GYOUSHA_CD_TO.Text;
            var genbaCd = this.GENBA_CD_TO.Text;

            if (string.IsNullOrWhiteSpace(genbaCd))
            {
                this.GENBA_NAME_TO.Text = String.Empty;
            }
            else if (!string.IsNullOrWhiteSpace(genbaCd))
            {
                if (!this.CheckGyoushaCd())
                {
                    e.Cancel = true;
                }
                else if (!this.CheckGenba(gyoushaCd, genbaCd, false))
                {
                    e.Cancel = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力された現場CDに対応する現場が存在するかチェックします
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="isFromCd">From側の現場CDをチェックする場合はTrue</param>
        /// <returns>チェック結果</returns>
        private bool CheckGenba(string gyoushaCd, string genbaCd, bool isFromCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, isFromCd);

            bool ret = true;

            try
            {
                var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                var mGenbaList = genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true });
                if (0 == mGenbaList.Count())
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");

                    if (isFromCd)
                    {
                        this.GENBA_NAME_FROM.Text = String.Empty;
                    }
                    else
                    {
                        this.GENBA_NAME_TO.Text = String.Empty;
                    }

                    ret = false;
                }
                else
                {
                    if (isFromCd)
                    {
                        this.GENBA_NAME_FROM.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.GENBA_NAME_TO.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msglogic.MessageBoxShow("E093");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msglogic.MessageBoxShow("E245");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CDが入力されているかをチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGyoushaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;

            try
            {
                var gyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                var gyoushaCdTo = this.GYOUSHA_CD_TO.Text;

                if (string.IsNullOrWhiteSpace(gyoushaCdFrom) && string.IsNullOrWhiteSpace(gyoushaCdTo))
                {
                    ret = false;

                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E012", "業者CD");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCd", ex);
                this.msglogic.MessageBoxShow("E245");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CD入力チェック
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckTorihikisaki(string torihikisakiCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd, isStartCd);

            bool ret = true;

            var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            var mTorihikisakiList = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd });
            if (0 == mTorihikisakiList.Count())
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");

                if (isStartCd)
                {
                    this.TORIHIKISAKI_NAME_FROM.Text = String.Empty;
                }
                else
                {
                    this.TORIHIKISAKI_NAME_TO.Text = String.Empty;
                }

                ret = false;
            }
            else
            {
                if (isStartCd)
                {
                    this.TORIHIKISAKI_NAME_FROM.Text = mTorihikisakiList.FirstOrDefault().TORIHIKISAKI_NAME_RYAKU;
                }
                else
                {
                    this.TORIHIKISAKI_NAME_TO.Text = mTorihikisakiList.FirstOrDefault().TORIHIKISAKI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CD入力チェック
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckGyousha(string gyoushaCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, isStartCd);

            bool ret = true;

            var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var mGyoushaList = gyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd });
            if (0 == mGyoushaList.Count())
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");

                if (isStartCd)
                {
                    this.GYOUSHA_NAME_FROM.Text = String.Empty;
                }
                else
                {
                    this.GYOUSHA_NAME_TO.Text = String.Empty;
                }

                ret = false;
            }
            else
            {
                if (isStartCd)
                {
                    this.GYOUSHA_NAME_FROM.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.GYOUSHA_NAME_TO.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private bool ChangeGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                var gyoushaCdTo = this.GYOUSHA_CD_TO.Text;

                if (string.IsNullOrWhiteSpace(gyoushaCdFrom) == false && string.IsNullOrWhiteSpace(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
                {
                    // 現場CDテキストボックスの活性状態を初期化
                    this.GENBA_CD_FROM.Enabled = true;
                    this.GENBA_NAME_FROM.Enabled = true;
                    this.GENBA_FROM_POPUP.Enabled = true;
                    this.GENBA_CD_TO.Enabled = true;
                    this.GENBA_NAME_TO.Enabled = true;
                    this.GENBA_TO_POPUP.Enabled = true;
                }
                else
                {
                    this.GENBA_CD_FROM.Enabled = false;
                    this.GENBA_NAME_FROM.Enabled = false;
                    this.GENBA_FROM_POPUP.Enabled = false;
                    this.GENBA_CD_TO.Enabled = false;
                    this.GENBA_NAME_TO.Enabled = false;
                    this.GENBA_TO_POPUP.Enabled = false;

                    this.GENBA_CD_FROM.Text = String.Empty;
                    this.GENBA_NAME_FROM.Text = String.Empty;
                    this.GENBA_CD_TO.Text = String.Empty;
                    this.GENBA_NAME_TO.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGenbaCdTextBoxEnabled", ex);
                this.msglogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDのゼロ埋め処理を行います
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ゼロ埋めした現場CD</returns>
        private String ZeroSuppressGenbaCd(String genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            var ret = String.Empty;
            if (string.IsNullOrWhiteSpace(genbaCd) == false)
            {
                ret = genbaCd.ToUpper().PadLeft(6, '0');
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者検索ボタン（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonSyukeiKomoku2StartMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        /// 業者検索ボタン（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonSyukeiKomoku2EndMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_CD_TO.Text = this.TORIHIKISAKI_CD_FROM.Text;
            this.TORIHIKISAKI_NAME_TO.Text = this.TORIHIKISAKI_NAME_FROM.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.GYOUSHA_CD_TO.Text = this.GYOUSHA_CD_FROM.Text;
            this.GYOUSHA_NAME_TO.Text = this.GYOUSHA_NAME_FROM.Text;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            this.GENBA_CD_TO.Text = this.GENBA_CD_FROM.Text;
            this.GENBA_NAME_TO.Text = this.GENBA_NAME_FROM.Text;
        }
    }
}