using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data;
using Microsoft.VisualBasic;

namespace Shougun.Core.PaperManifest.Manifestsuiihyo.APP
{
    public partial class JoukenPopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private LogicClassJouken logic;

        /// <summary>
        /// 検索条件クラス
        /// </summary>
        public JoukenParam joken { get; set; }
        #endregion

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="joukenParam">イベントデータ</param>
        public JoukenPopupForm(JoukenParam joukenParam)
        {
            InitializeComponent();

            // ロジッククラス作成
            logic = new LogicClassJouken(this);

            this.joken = joukenParam;
        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.logic.WindowInit(this.joken);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void JoukenPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    // 検索実行
                    this.SearchExec(sender, null);
                    break;
                case Keys.F12:
                    this.FormClose(sender, null);
                    break;
                default:
                    // NOTHING
                    break;
            }
        }

        /// <summary>
        /// 年月日Fromのチェック
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmDateTimePicker_NengappiFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            // 年月日Fromが未入力場合
            if (this.cstmDateTimePicker_NengappiFrom.Value == null)
            {
                messageShowLogic.MessageBoxShow("E001", "年月日");
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 年月日Toのチェック
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmDateTimePicker_NengappiTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            // 年月日Toが未入力場合
            if (this.cstmDateTimePicker_NengappiTo.Value == null)
            {
                messageShowLogic.MessageBoxShow("E001", "年月日");
                this.cstmDateTimePicker_NengappiTo.Focus();
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 検索実行処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void SearchExec(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            DateTime dateFrom;
            DateTime dateTo;

            if (DateTime.TryParse(this.cstmDateTimePicker_NengappiFrom.Value.ToString(), out dateFrom)
                && DateTime.TryParse(this.cstmDateTimePicker_NengappiTo.Value.ToString(), out dateTo))
            {
                // TO - FROMが１２か月を超えていたら強制的にToの値を変えて１２か月にする
                long monthsBetween = DateAndTime.DateDiff(DateInterval.Month, dateFrom, dateTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                if (monthsBetween >= 12)
                {
                    messageShowLogic.MessageBoxShow("E002", "年月日", "12ヶ月");
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.logic.SaveParams();
            // Formクローズ
            this.FormClose(sender, null);
        }

        /// <summary>
        /// FormClosing
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void JoukenPopupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            DateTime dateFrom;
            DateTime dateTo;

            if (DateTime.TryParse(this.cstmDateTimePicker_NengappiFrom.Value.ToString(), out dateFrom)
                && DateTime.TryParse(this.cstmDateTimePicker_NengappiTo.Value.ToString(), out dateTo))
            {
                // TO - FROMが１２か月を超えていたら強制的にToの値を変えて１２か月にする
                long monthsBetween = DateAndTime.DateDiff(DateInterval.Month, dateFrom, dateTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                if (monthsBetween >= 12)
                {
                    this.cstmDateTimePicker_NengappiFrom.Focus();
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            // Formクローズ
            this.Close();
        }

        /// <summary>
        /// 一次二次区分のロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmNmTxtB_IchijiNiji_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmNmTxtB_IchijiNiji.Text))
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                // エラーメッセージ表示
                messageShowLogic.MessageBoxShow("E002", "一次二次区分", "1～2");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 排出事業者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaishutuJigyoushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmTexBox_HaishutuJigyoushaFrom.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("排出事業者CDFrom"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "業者");

                    this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "1"))
            {
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDFromポップアップ後設定処理
        /// </summary>
        public void SetGyoushaFromPop()
        {
            // 排出事業場CD制御
            this.HaisyutsuJigyoubaSeigyo();
        }

        /// <summary>
        /// 排出事業者CDFromボタンポップアップ後設定処理
        /// </summary>
        public void SetGyoushaFromPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "1"))
            {
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaishutuJigyoushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaTo.Text))
            {
                this.cstmTexBox_HaishutuJigyoushaTo.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("排出事業者CDTo"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_HaishutuJigyoushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "2"))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDToポップアップ後設定処理
        /// </summary>
        public void SetGyoushaToPop()
        {
            // 排出事業場CD制御
            this.HaisyutsuJigyoubaSeigyo();
        }

        /// <summary>
        /// 排出事業者CDToボタンポップアップ後設定処理
        /// </summary>
        public void SetGyoushaToPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "2"))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// FromCDとToCDのチェック
        /// </summary>
        /// <param name="fromVal">FromValue</param>
        /// <param name="toVal">ToValue</param>
        /// <param name="msgKbn">
        /// エラーメッセージ区分:1　fromVal > toVal
        ///                      2　toVal > fromVal
        /// </param>
        private bool ChkFromAndTo(string fromVal, string toVal, string msgKbn)
        {
            // 開始コード ＞ 終了コードの場合
            if (!string.IsNullOrEmpty(fromVal) && !string.IsNullOrEmpty(toVal)
                && fromVal.CompareTo(toVal) > 0)
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                if ("1".Equals(msgKbn))
                {
                    messageShowLogic.MessageBoxShow("E032", "終了コード", "開始コード");
                }
                else
                {
                    messageShowLogic.MessageBoxShow("E032", "終了コード", "開始コード");
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 排出事業場の設定
        /// </summary>
        private void HaisyutsuJigyoubaSeigyo()
        {
            // 排出事業者CDFromと排出事業者CDToが両方未入力の場合
            // 排出事業者CDFromと排出事業者CDToが同一の場合
            if ((string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text)
                && string.IsNullOrEmpty(cstmANTexB_HaishutuJigyoushaTo.Text))
                || (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text)
                && !string.IsNullOrEmpty(cstmANTexB_HaishutuJigyoushaTo.Text)
                && this.cstmANTexB_HaishutuJigyoushaFrom.Text.Equals(this.cstmANTexB_HaishutuJigyoushaTo.Text)))
            {
                this.customPanel_HaisyutsuJigyouba.Enabled = true;
            }
            else
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
                this.customPanel_HaisyutsuJigyouba.Enabled = false;
            }
        }

        /// <summary>
        /// 排出事業場CDFromのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaFrom_Enter(object sender, EventArgs e)
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "GYOUSHA_CD";
                searchDto1.Value = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);
                methodDto.SearchCondition.Add(searchDto1);

                this.cstmANTexB_HaisyutsuJigyoubaFrom.popupWindowSetting.Clear();
                this.cstmANTexB_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto);
            }
            else
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                searchDto1.Value = "True";
                searchDto1.ValueColumnType = DB_TYPE.BIT;

                methodDto1.Join = JOIN_METHOD.WHERE;
                methodDto1.LeftTable = "M_GYOUSHA";
                methodDto1.SearchCondition.Add(searchDto1);

                this.cstmANTexB_HaisyutsuJigyoubaFrom.popupWindowSetting.Clear();
                this.cstmANTexB_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto);
                this.cstmANTexB_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto1);
            }
        }

        /// <summary>
        /// 排出事業場CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {        
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text)
                && string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                messageShowLogic.MessageBoxShow("E051", "排出事業者");
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                return;
            }


            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text))
            {
                this.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGenba("排出事業場CDFrom"))
                {

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "1"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
            }
        }

        /// <summary>
        /// 排出事業場CDFromポップアップ後設定処理
        /// </summary>
        public void SetGenbaFromPop()
        {
            // 排出事業者CDFromが設定される場合
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
            }
        }

        /// <summary>
        /// 排出事業場CDFromボタンポップアップ後設定処理
        /// </summary>
        public void SetGenbaFromPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "1"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
            }
            else
            {
                // 排出事業者CDFromが設定される場合
                if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
                {
                    this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                    this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
                }
            }
        }

        /// <summary>
        /// 排出事業場CDToのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaTo_Enter(object sender, EventArgs e)
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "GYOUSHA_CD";
                searchDto1.Value = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);
                methodDto.SearchCondition.Add(searchDto1);

                this.cstmANTexB_HaisyutsuJigyoubaTo.popupWindowSetting.Clear();
                this.cstmANTexB_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto);
            }
            else
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                searchDto1.Value = "True";
                searchDto1.ValueColumnType = DB_TYPE.BIT;

                methodDto1.Join = JOIN_METHOD.WHERE;
                methodDto1.LeftTable = "M_GYOUSHA";
                methodDto1.SearchCondition.Add(searchDto1);

                this.cstmANTexB_HaisyutsuJigyoubaTo.popupWindowSetting.Clear();
                this.cstmANTexB_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto);
                this.cstmANTexB_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto1);
            }
        }

        /// <summary>
        /// 排出事業場CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaTo.Text)
                && string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                messageShowLogic.MessageBoxShow("E051", "排出事業者");
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                return;
            }

            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaTo.Text))
            {
                this.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGenba("排出事業場CDTo"))
                {

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "2"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
            }
        }

        /// <summary>
        /// 排出事業場CDToポップアップ後設定処理
        /// </summary>
        public void SetGenbaToPop()
        {
            // 排出事業者CDFromが設定される場合
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
            }
        }

        /// <summary>
        /// 排出事業場CDToボタンポップアップ後設定処理
        /// </summary>
        public void SetGenbaToPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "2"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
            }
            else
            {
                // 排出事業者CDFromが設定される場合
                if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
                {
                    this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                    this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
                }
            }
        }

        /// <summary>
        /// 運搬受託者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_UnpanJutakushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_UnpanJutakushaFrom.Text))
            {
                this.cstmTexBox_UnpanJutakushaFrom.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("運搬受託者CDFrom"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_UnpanJutakushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_UnpanJutakushaFrom.Text,
                this.cstmANTexB_UnpanJutakushaTo.Text, "1"))
            {
                this.cstmANTexB_UnpanJutakushaFrom.Focus();
            }
        }

        /// <summary>
        /// 運搬受託者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_UnpanJutakushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_UnpanJutakushaTo.Text))
            {
                this.cstmTexBox_UnpanJutakushaTo.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("運搬受託者CDTo"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_UnpanJutakushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_UnpanJutakushaFrom.Text,
                this.cstmANTexB_UnpanJutakushaTo.Text, "2"))
            {
                this.cstmANTexB_UnpanJutakushaTo.Focus();
            }
        }

        /// <summary>
        /// 処分受託者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_ShobunJutakushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_ShobunJutakushaFrom.Text))
            {
                this.cstmTexBox_ShobunJutakushaFrom.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("処分受託者CDFrom"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_ShobunJutakushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_ShobunJutakushaFrom.Text,
                this.cstmANTexB_ShobunJutakushaTo.Text, "1"))
            {
                this.cstmANTexB_ShobunJutakushaFrom.Focus();
            }
        }

        /// <summary>
        /// 処分受託者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_ShobunJutakushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_ShobunJutakushaTo.Text))
            {
                this.cstmTexBox_ShobunJutakushaTo.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGyousha("処分受託者CDTo"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_ShobunJutakushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_ShobunJutakushaFrom.Text,
                this.cstmANTexB_ShobunJutakushaTo.Text, "2"))
            {
                this.cstmANTexB_ShobunJutakushaTo.Focus();
            }
        }

        /// <summary>
        /// 最終処分事業場CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_SaishuuShobunJouFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_SaishuuShobunJouFrom.Text))
            {
                this.cstmTexBox_SaishuuShobunJouFrom.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGenba("最終処分事業場CDFrom"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_SaishuuShobunJouFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_SaishuuShobunJouFrom.Text,
                this.cstmANTexB_SaishuuShobunJouTo.Text, "1"))
            {
                this.cstmANTexB_SaishuuShobunJouFrom.Focus();
            }
        }

        /// <summary>
        /// 最終処分事業場CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_SaishuuShobunJouTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_SaishuuShobunJouTo.Text))
            {
                this.cstmTexBox_SaishuuShobunJouTo.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkGenba("最終処分事業場CDTo"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_SaishuuShobunJouTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_SaishuuShobunJouFrom.Text,
                this.cstmANTexB_SaishuuShobunJouTo.Text, "2"))
            {
                this.cstmANTexB_SaishuuShobunJouTo.Focus();
            }
        }

        /// <summary>
        /// 産廃（直行）廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Chokkou_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Chokkou.Text))
            {
                this.cstmTexBox_Chokkou.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkHaikiShurui("直行"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Chokkou.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 産廃（積替）廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Tsumikae_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Tsumikae.Text))
            {
                this.cstmTexBox_Tsumikae.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkHaikiShurui("積替"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Tsumikae.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 建廃廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Kenpai_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Kenpai.Text))
            {
                this.cstmTexBox_Kenpai.Text = string.Empty;
            }
            else
            {
                if (!this.logic.ChkHaikiShurui("建廃"))
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Kenpai.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Denshi_Enter(object sender, EventArgs e)
        {
            //検索結果
            DataTable dtSearch = new DataTable();
            DataTable dtResult = new DataTable();

            DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);

            dtResult.Columns.Add("HAIKI_SHURUI_CD");
            dtResult.Columns.Add("HAIKI_SHURUI_NAME");
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = dtSearch.Rows[i]["HAIKI_SHURUI_CD"];
                dr[1] = dtSearch.Rows[i]["HAIKI_SHURUI_NAME"];
                dtResult.Rows.Add(dr);
            }

            //データが存在する場合
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                this.cstmANTexB_Denshi.PopupDataHeaderTitle = new string[] { "廃棄物種類コード", "廃棄物種類名" };
                this.cstmANTexB_Denshi.PopupDataSource = dtResult;
                this.cstmANTexB_Denshi.PopupDataSource.TableName = "廃棄物種類検索";
            }
        }

        /// <summary>
        /// 電子廃棄物種類検索ボタンのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void casbtn_Denshi_Enter(object sender, EventArgs e)
        {
            //検索結果
            DataTable dtSearch = new DataTable();
            DataTable dtResult = new DataTable();

            DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);
            dtResult.Columns.Add("HAIKI_SHURUI_CD");
            dtResult.Columns.Add("HAIKI_SHURUI_NAME");
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = dtSearch.Rows[i]["HAIKI_SHURUI_CD"];
                dr[1] = dtSearch.Rows[i]["HAIKI_SHURUI_NAME"];
                dtResult.Rows.Add(dr);
            }

            //データが存在する場合
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                this.casbtn_Denshi.PopupDataHeaderTitle = new string[] { "廃棄物種類コード", "廃棄物種類名" };
                this.casbtn_Denshi.PopupDataSource = dtResult;
                this.casbtn_Denshi.PopupDataSource.TableName = "廃棄物種類検索";
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Denshi_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Denshi.Text))
            {
                this.cstmTexBox_Denshi.Text = string.Empty;
            }
            else
            {

                //検索結果
                DataTable dtSearch = new DataTable();

                DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

                dto.HAIKI_SHURUI_CD = this.cstmANTexB_Denshi.Text;
                dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);

                if (dtSearch.Rows.Count == 0)
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    this.cstmTexBox_Denshi.Text = string.Empty;
                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.cstmTexBox_Denshi.Text = dtSearch.Rows[0]["HAIKI_SHURUI_NAME"].ToString();
                }
            }
        }

        /// <summary>
        /// 排出事業場CDFromのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void casbtn_HaisyutsuJigyoubaFrom_Enter(object sender, EventArgs e)
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "GYOUSHA_CD";
                searchDto1.Value = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);
                methodDto.SearchCondition.Add(searchDto1);

                this.casbtn_HaisyutsuJigyoubaFrom.popupWindowSetting.Clear();
                this.casbtn_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto);
            }
            else
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                searchDto1.Value = "True";
                searchDto1.ValueColumnType = DB_TYPE.BIT;

                methodDto1.Join = JOIN_METHOD.WHERE;
                methodDto1.LeftTable = "M_GYOUSHA";
                methodDto1.SearchCondition.Add(searchDto1);

                this.casbtn_HaisyutsuJigyoubaFrom.popupWindowSetting.Clear();
                this.casbtn_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto);
                this.casbtn_HaisyutsuJigyoubaFrom.popupWindowSetting.Add(methodDto1);
            }
        }

        /// <summary>
        /// 排出事業場CDToのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void casbtn_HaisyutsuJigyoubaTo_Enter(object sender, EventArgs e)
        {
            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "GYOUSHA_CD";
                searchDto1.Value = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);
                methodDto.SearchCondition.Add(searchDto1);

                this.casbtn_HaisyutsuJigyoubaTo.popupWindowSetting.Clear();
                this.casbtn_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto);
            }
            else
            {
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                searchDto.Value = "True";
                searchDto.ValueColumnType = DB_TYPE.BIT;

                methodDto.Join = JOIN_METHOD.WHERE;
                methodDto.LeftTable = "M_GENBA";
                methodDto.SearchCondition.Add(searchDto);

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                searchDto1.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                searchDto1.Value = "True";
                searchDto1.ValueColumnType = DB_TYPE.BIT;

                methodDto1.Join = JOIN_METHOD.WHERE;
                methodDto1.LeftTable = "M_GYOUSHA";
                methodDto1.SearchCondition.Add(searchDto1);

                this.casbtn_HaisyutsuJigyoubaTo.popupWindowSetting.Clear();
                this.casbtn_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto);
                this.casbtn_HaisyutsuJigyoubaTo.popupWindowSetting.Add(methodDto1);
            }
        }

        /// <summary>
        /// 出力内容のロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmNmTxtB_ShuturyokuNaiyou_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 入力されない場合
            if (string.IsNullOrEmpty(this.cstmNmTxtB_ShuturyokuNaiyou.Text))
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E002", "出力内容", "1～5");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 出力区分のロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmNmTxtB_ShuturyokuKubun_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 入力されない場合
            if (string.IsNullOrEmpty(this.cstmNmTxtB_ShuturyokuKubun.Text))
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E002", "出力区分", "1～3");
                e.Cancel = true;
            }
        }

        /// <summary> 廃棄物種類活性非活性制御 </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <remark>出力区分：合算⇒すべて活性</remark>
        /// <remark>出力区分：紙のみ⇒電子のみ非活性</remark>
        /// <remark>出力区分：電子のみ⇒電子のみ活性</remark>
        private void cstmNmTxtB_ShuturyokuKubun_TextChanged(object sender, EventArgs e)
        {
            string shuturyokuKubun = this.cstmNmTxtB_ShuturyokuKubun.Text;

            // 出力区分により制御する
            switch (shuturyokuKubun)
            {
                case "1":                                                       // 合算
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = true;       // 直行：活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = true;      // 積替：活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = true;        // 建廃：活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = true;        // 電子：活性
                    this.cstmpanel_Kyoten.Enabled = true;                       // 拠点：活性
                    break;

                case "2":                                                       // 紙のみ
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = true;       // 直行：活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = true;      // 積替：活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = true;        // 建廃：活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = false;       // 電子：非活性
                    this.cstmpanel_Kyoten.Enabled = true;                       // 拠点：活性
                    break;

                case "3":                                                       // 電子のみ
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = false;      // 直行：非活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = false;     // 積替：非活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = false;       // 建廃：非活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = true;        // 電子：活性
                    this.cstmpanel_Kyoten.Enabled = false;                      // 拠点：非活性
                    break;

                default:
                    break;
            }
        }
    }
}
