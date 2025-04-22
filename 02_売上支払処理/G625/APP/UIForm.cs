using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    /// <summary>
    /// 支払順位表画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 支払順位表画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// パターンDTOリスト
        /// </summary>
        private List<PatternDto> patternDtoList;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
        {
            this.InitializeComponent();

            this.WindowId = WINDOW_ID.T_SHIHARAI_JUNIHYO;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }
            if (!this.SetWindowId(this.WindowId))
            {
                return;
            }
            this.SetDefault();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面の初期値をセットします
        /// </summary>
        private void SetDefault()
        {
            this.HIDUKE_SHURUI.Text = ConstClass.DATE_CD_DENPYOU;
            this.HIDUKE.Text = ConstClass.DATE_RANGE_CD_DAY;
            this.DENPYOU_SHURUI.Text = ConstClass.DENPYOU_SHURUI_CD_ALL;
            this.TORIHIKI_KBN.Text = ConstClass.TORIHIKI_KBN_CD_ALL;
            this.KAKUTEI_KBN.Text = ConstClass.KAKUTEI_KBN_CD_ALL;
            this.SHIME_KBN.Text = ConstClass.SHIME_KBN_CD_ALL;

            this.KYOTEN_CD.Text = ConstClass.KYOTEN_CD_ALL;
            this.KYOTEN_NAME.Text = ConstClass.KYOTEN_NAME_ALL;
        }

        /// <summary>
        /// [F1]新規ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var patternDto = new PatternDto();
            patternDto.Pattern.WINDOW_ID = (int)this.WindowId;
            this.ShowTourokuPopup(WINDOW_TYPE.NEW_WINDOW_FLAG, patternDto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F2]修正ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F4]削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc4_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.DELETE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F5]CSV
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (dtos == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/07 #136228
                return;
            }

            if (!this.RegistErrorFlag)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                if (this.IsErrorDateFromTo(this.HIDUKE_FROM, this.HIDUKE_TO))
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.HIDUKE_FROM.DisplayItemName, this.HIDUKE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (dtos != null)
                {
                    var count = this.logic.Search();
                    if (0 < count)
                    {
                        if (!this.logic.CSV())
                        {
                            return;
                        }
                    }
                    else if (count == 0)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("C001");
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (this.RegistErrorFlag == true)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }

            this.RecoveryFocusOutCheckMethod();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]表示ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (dtos == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/07 #136228
                return;
            }

            if (!this.RegistErrorFlag)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                if (this.IsErrorDateFromTo(this.HIDUKE_FROM, this.HIDUKE_TO))
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.HIDUKE_FROM.DisplayItemName, this.HIDUKE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (dtos != null)
                {
                    var count = this.logic.Search();
                    if (0 < count)
                    {
                        if (!this.logic.CreateJunihyo())
                        {
                            return;
                        }
                    }
                    else if (count == 0)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("C001");
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (this.RegistErrorFlag == true)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }

            this.RecoveryFocusOutCheckMethod();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付範囲テキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TextChanged(object sender, EventArgs e)
        {
            if (this.HIDUKE.Text == ConstClass.DATE_RANGE_CD_SHITEI)
            {
                // 指定
                this.HIDUKE_FROM.Enabled = true;
                this.HIDUKE_TO.Enabled = true;
                this.HIDUKE_FROM.Value = null;
                this.HIDUKE_TO.Value = null;
            }
            else if (this.HIDUKE.Text == ConstClass.DATE_RANGE_CD_MONTH)
            {
                // 当月
                this.HIDUKE_FROM.Enabled = false;
                this.HIDUKE_TO.Enabled = false;
                this.HIDUKE_FROM.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1);
                this.HIDUKE_TO.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, DateTime.DaysInMonth(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month));

            }
            else if (this.HIDUKE.Text == ConstClass.DATE_RANGE_CD_DAY)
            {
                // 当日
                this.HIDUKE_FROM.Enabled = false;
                this.HIDUKE_TO.Enabled = false;
                this.HIDUKE_FROM.Value = this.logic.parentForm.sysDate;
                this.HIDUKE_TO.Value = this.logic.parentForm.sysDate;
            }
            else
            {
                this.HIDUKE_FROM.Enabled = false;
                this.HIDUKE_TO.Enabled = false;
                this.HIDUKE_FROM.Value = null;
                this.HIDUKE_TO.Value = null;
            }
        }

        /// <summary>
        /// パターンリストボックスの選択Indexが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void PATTERN_LIST_BOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (PatternDto)this.PATTERN_LIST_BOX.SelectedItem;
            if (selectedItem == null)
            {
                this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>();
            }
            else
            {
                var select1 = selectedItem.GetColumnSelect(1);
                if (select1 == null)
                {
                    select1 = new S_LIST_COLUMN_SELECT();
                }
                var select2 = selectedItem.GetColumnSelect(2);
                if (select2 == null)
                {
                    select2 = new S_LIST_COLUMN_SELECT();
                }
                this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>() { select1 };
                this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>() { select2 };
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンリストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void PATTERN_LIST_BOX_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null)
            {
                this.KYOTEN_CD.Focus();
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付範囲 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!String.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_TO.Text = this.HIDUKE_FROM.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者 Fromテキストの値が変更した時に処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者 Toテキストの値が変更した時に処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業担当者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運転者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 品名 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運転者CD From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.UNTENSHA_CD_FROM.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNTENSHA_CD_FROM.Text))
                {
                    // 運転者名の初期化は行う
                    this.UNTENSHA_NAME_FROM.Text = string.Empty;
                    return;
                }

                this.logic.UNTENSHA_CDValidated(this.UNTENSHA_CD_FROM, this.UNTENSHA_NAME_FROM);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運転者CD To
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.UNTENSHA_CD_TO.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNTENSHA_CD_TO.Text))
                {
                    // 運転者名の初期化は行う
                    this.UNTENSHA_NAME_TO.Text = string.Empty;
                    return;
                }

                this.logic.UNTENSHA_CDValidated(this.UNTENSHA_CD_TO, this.UNTENSHA_NAME_TO);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// パターン登録ポップアップを表示します
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="dto">パターンDTOクラス</param>
        private bool ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, dto);

                var popup = new ChouhyouPatternTourokuPopupForm(windowType, dto);
                var dialogResult = popup.ShowDialog();
                popup.Dispose();

                if (DialogResult.Cancel != dialogResult)
                {
                    // ポップアップを閉じたらパターンのリストを再読込み
                    this.SetWindowId(this.WindowId);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTourokuPopup", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 出力する帳票の画面IDをセットします
        /// </summary>
        /// <param name="windowId">画面ID</param>
        private bool SetWindowId(WINDOW_ID windowId)
        {
            try
            {
                LogUtility.DebugMethodStart(windowId);

                this.patternDtoList = new List<PatternDto>();

                // セットされた画面IDで登録されているパターンを取得
                var patternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>();
                var patternColumnDao = DaoInitUtility.GetComponent<IM_LIST_PATTERN_COLUMNDao>();
                var columnSelectDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECTDao>();
                var columnSelectDetailDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECT_DETAILDao>();
                var patternList = patternDao.GetMListPatternList((int)windowId);
                foreach (var pattern in patternList)
                {
                    var dto = new PatternDto();
                    dto.Pattern = pattern;
                    dto.PatternColumnList = patternColumnDao.GetMListPatternColumnList(pattern.SYSTEM_ID.Value, pattern.SEQ.Value);
                    foreach (var patternColumn in dto.PatternColumnList)
                    {
                        dto.ColumnSelectList.Add(columnSelectDao.GetSListColumnSelectList(new S_LIST_COLUMN_SELECT() { WINDOW_ID = patternColumn.WINDOW_ID, KOUMOKU_ID = patternColumn.KOUMOKU_ID }).FirstOrDefault());
                        dto.ColumnSelectDetailList.Add(columnSelectDetailDao.GetSListColumnSelectDetailList(new S_LIST_COLUMN_SELECT_DETAIL() { WINDOW_ID = patternColumn.WINDOW_ID, KOUMOKU_ID = patternColumn.KOUMOKU_ID }).FirstOrDefault());
                    }

                    this.patternDtoList.Add(dto);
                }

                this.PATTERN_LIST_BOX.DataSource = this.patternDtoList;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetWindowId", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowId", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private void RecoveryFocusOutCheckMethod()
        {
            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            this.GYOUSHA_CD_FROM.CausesValidation = true;
            this.GYOUSHA_CD_TO.CausesValidation = true;
            this.GENBA_CD_FROM.CausesValidation = true;
            this.GENBA_CD_TO.CausesValidation = true;
            this.EIGYOU_TANTOUSHA_CD_FROM.CausesValidation = true;
            this.EIGYOU_TANTOUSHA_CD_TO.CausesValidation = true;
            this.UNTENSHA_CD_FROM.CausesValidation = true;
            this.UNTENSHA_CD_TO.CausesValidation = true;
            this.HINMEI_CD_FROM.CausesValidation = true;
            this.HINMEI_CD_TO.CausesValidation = true;
            this.BUNRUI_CD_FROM.CausesValidation = true;
            this.BUNRUI_CD_TO.CausesValidation = true;
            this.SHURUI_CD_FROM.CausesValidation = true;
            this.SHURUI_CD_TO.CausesValidation = true;

            //PhuocLoc 2020/12/07 #136228 -Start
            this.SHUUKEI_KOUMOKU_CD_FROM.CausesValidation = true;
            this.SHUUKEI_KOUMOKU_CD_TO.CausesValidation = true;
            //PhuocLoc 2020/12/07 #136228 -End
        }

        /// <summary>
        /// FromテキストボックスからToテキストボックスにCDと名称をコピーします
        /// </summary>
        /// <param name="sender">CdFromテキストボックス</param>
        private bool SetToCdAndName(object sender)
        {
            try
            {
                LogUtility.DebugMethodStart(sender);

                var cdToTextBox = (TextBox)sender;
                var cdFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_TO", "_FROM")).First();
                if (!String.IsNullOrEmpty(cdFromTextBox.Text))
                {
                    cdToTextBox.Text = cdFromTextBox.Text;

                    var nameFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_FROM")).First();
                    var nameToTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_TO")).First();
                    nameToTextBox.Text = nameFromTextBox.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetToCdAndName", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private bool ChangeGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var fromCd = this.GYOUSHA_CD_FROM.Text;
                var toCd = this.GYOUSHA_CD_TO.Text;

                var enabled = !String.IsNullOrEmpty(fromCd) &&
                              !String.IsNullOrEmpty(toCd) &&
                              this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd);

                if (!enabled)
                {
                    // 非活性時は初期化
                    this.GENBA_CD_FROM.Text = String.Empty;
                    this.GENBA_CD_TO.Text = String.Empty;
                    this.GENBA_NAME_FROM.Text = String.Empty;
                    this.GENBA_NAME_TO.Text = String.Empty;
                }

                this.GENBA_CD_FROM.Enabled = enabled;
                this.GENBA_CD_TO.Enabled = enabled;
                this.GENBA_FROM_POPUP.Enabled = enabled;
                this.GENBA_TO_POPUP.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGenbaCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
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
            if (String.IsNullOrEmpty(genbaCd) == false)
            {
                ret = genbaCd.ToUpper().PadLeft(6, '0');
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 日付範囲のFromToチェックを行います
        /// </summary>
        /// <param name="fromTextBox">日付Fromテキストボックス</param>
        /// <param name="toTextBox">日付Toテキストボックス</param>
        /// <returns>エラーがある場合は、True</returns>
        private bool IsErrorDateFromTo(CustomDateTimePicker fromTextBox, CustomDateTimePicker toTextBox)
        {
            LogUtility.DebugMethodStart(fromTextBox, toTextBox);

            var ret = false;

            var fromDate = fromTextBox.Text;
            var toDate = toTextBox.Text;
            if (String.IsNullOrEmpty(fromDate) == false && String.IsNullOrEmpty(toDate) == false)
            {
                if (fromDate.CompareTo(toDate) > 0)
                {
                    fromTextBox.IsInputErrorOccured = true;
                    toTextBox.IsInputErrorOccured = true;
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// CSV
        /// </summary>
        public void CsvReport(DataTable dt)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SHIHARAI_JYUNNIHYOU), this);
            }
        }

        //PhuocLoc 2020/12/07 #136228 -Start
        private void SHUUKEI_KOUMOKU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }
        //PhuocLoc 2020/12/07 #136228 -End
    }
}
