using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Reception.UketsukeMeisaihyo
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>受付明細表 ロジッククラス</summary>
        private LogicClass logic { set; get; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region メソッド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">r_framework.Const.WINDOW_ID : 受付明細表)</param>
        public UIForm(WINDOW_ID windowID)
            : base(windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new LogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region OnLoadEvent

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
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

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ShownEnvent

        /// <summary>
        /// 初回起動時に画面が表示された後で処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を拠点CDにする
            this.KYOTEN_CD.Focus();
        }

        #endregion

        #region [F5] CSVボタン ClickEvent

        /// <summary>
        /// [F5] CSVボタン ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            // RegistCheck実行
            if (!this.logic.RegistCheck())
            {
                return;
            }

            // [F5] CSV出力
            this.logic.CSVPrint();
        }

        #endregion

        #region [F7] 表示ボタン ClickEvent

        /// <summary>
        /// [F7] 表示ボタン ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            // RegistCheck実行
            if (!this.logic.RegistCheck())
            {
                return;
            }

            // [F7] 表示処理
            this.logic.ButtonFunc7_Clicked();
        }

        #endregion

        #region [F12] 閉じるボタン ClickEvent

        /// <summary>
        /// [F12] 閉じるボタン ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 日付範囲ラジオボタン CheckedChangedEvent

        /// <summary>
        /// 日付範囲ラジオボタン CheckedChangedEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_RANGE_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_RANGE_FROM.Text = String.Empty;
            this.HIDUKE_RANGE_TO.Text = String.Empty;

            if (this.HIDUKE_RANGE_KIKAN.Checked)
            {
                // 期間指定がチェックされている場合は日付範囲項目を活性化
                this.customPanelHidukeHaniShitei.Enabled = true;
            }
            else
            {
                // 期間指定以外がチェックされている場合は日付範囲項目を非活性化
                this.customPanelHidukeHaniShitei.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 業者CDFrom、業者CDTo TextChangedEvent

        /// <summary>
        /// 業者CDFrom、業者CDTo TextChangedEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.CheckGgyoushaIsNotNullAndSameValue())
            {
                // 業者が一意の場合、現場を活性化
                this.logic.SetGenbaEnabled(true);
            }
            else
            {
                // 業者が一意ではない場合、現場を非活性化
                this.logic.SetGenbaEnabled(false);

                // 現場CD・名クリア
                this.GENBA_CD_FROM.Text = string.Empty;
                this.GENBA_NAME_FROM.Text = string.Empty;
                this.GENBA_CD_TO.Text = string.Empty;
                this.GENBA_NAME_TO.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 現場CDFrom、現場CDTo ValidatingEvent

        /// <summary>
        /// 現場CDFrom、現場CDTo ValidatingEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 現場存在チェック＆名称設定
            this.logic.GENBA_CD_Validating(sender, e);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 運搬業者CDFrom、運搬業者CDTo TextChangedEvent

        /// <summary>
        /// 運搬業者CDFrom、運搬業者CDTo TextChangedEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.CheckUnpanGyoushaIsNotNullAndSameValue())
            {
                // 運搬業者が一意の場合、現場を活性化
                this.logic.SetSharyouEnabled(true);
            }
            else
            {
                // 運搬業者が一意ではない場合、現場を非活性化
                this.logic.SetSharyouEnabled(false);

                // 現場CD・名クリア
                this.SHARYOU_CD_FROM.Text = string.Empty;
                this.SHARYOU_NAME_FROM.Text = string.Empty;
                this.SHARYOU_CD_TO.Text = string.Empty;
                this.SHARYOU_NAME_TO.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 運搬業者CDFrom、運搬業者CDTo ValidatingEvent

        /// <summary>
        /// 運搬業者CDFrom、運搬業者CDTo ValidatingEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 運搬業者存在チェック＆名称設定
            this.logic.UNPAN_GYOUSHA_CD_Validating(sender, e);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 車輌CDFrom、車輌CDTo ValidatingEvent

        /// <summary>
        /// 車輌CDFrom、車輌CDTo ValidatingEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 車輌存在チェック＆名称設定
            this.logic.SHARYOU_CD_Validating(sender, e);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region From～ToのToコントロール DoubleClickEvent : From値をTo値にコピーする

        /// <summary>
        /// From～ToのToコントロール DoubleClickEvent
        /// From値をTo値にコピーします
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TO_Control_DoubleClick(object sender, EventArgs e)
        {
            var control = (Control)sender;

            if(control.Name.Equals(this.HIDUKE_RANGE_TO.Name))
            {
                // 日付
                this.HIDUKE_RANGE_TO.Value = this.HIDUKE_RANGE_FROM.Value;
            }
            else if (control.Name.Equals(this.TORIHIKISAKI_CD_TO.Name))
            {
                // 取引先
                this.TORIHIKISAKI_CD_TO.Text = this.TORIHIKISAKI_CD_FROM.Text;
                this.TORIHIKISAKI_NAME_TO.Text = this.TORIHIKISAKI_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.GYOUSHA_CD_TO.Name))
            {
                // 業者
                this.GYOUSHA_CD_TO.Text = this.GYOUSHA_CD_FROM.Text;
                this.GYOUSHA_NAME_TO.Text = this.GYOUSHA_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.GENBA_CD_TO.Name) && this.GENBA_CD_FROM.Enabled && this.GENBA_CD_TO.Enabled)
            {
                // 現場
                this.GENBA_CD_TO.Text = this.GENBA_CD_FROM.Text;
                this.GENBA_NAME_TO.Text = this.GENBA_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.UNPAN_GYOUSHA_CD_TO.Name))
            {
                // 運搬業者
                this.UNPAN_GYOUSHA_CD_TO.Text = this.UNPAN_GYOUSHA_CD_FROM.Text;
                this.UNPAN_GYOUSHA_NAME_TO.Text = this.UNPAN_GYOUSHA_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.SHASHU_CD_TO.Name))
            {
                // 車種
                this.SHASHU_CD_TO.Text = this.SHASHU_CD_FROM.Text;
                this.SHASHU_NAME_TO.Text = this.SHASHU_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.SHARYOU_CD_TO.Name) && this.SHARYOU_CD_TO.Enabled && this.SHARYOU_CD_FROM.Enabled)
            {
                // 車輌
                this.SHARYOU_CD_TO.Text = this.SHARYOU_CD_FROM.Text;
                this.SHARYOU_NAME_TO.Text = this.SHARYOU_NAME_FROM.Text;
            }
            else if (control.Name.Equals(this.UNTENSHA_CD_TO.Name))
            {
                // 運転者
                this.UNTENSHA_CD_TO.Text = this.UNTENSHA_CD_FROM.Text;
                this.UNTENSHA_NAME_TO.Text = this.UNTENSHA_NAME_FROM.Text;
            }
        }

        #endregion

        #region 運転者CDFrom、運転者CDTo ValidatingEvent

        private void UNTENSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 運転者存在チェック＆名称設定
            this.logic.UNTENSHA_CD_Validating(sender, e);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #endregion
    }
}
