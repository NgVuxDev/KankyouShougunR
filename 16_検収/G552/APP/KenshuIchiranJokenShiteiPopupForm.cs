using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Const;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup
{
    public partial class KenshuIchiranJokenShiteiPopupForm : SuperForm
    {
        UIHeader header_new;

        //フィールド
        public KenshuIshiranConstans.ConditionInfo Joken { get; set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private KenshuIchiranJokenShiteiPopupLogic kenshuIchiranJokenShiteiPopupLogic;

        /// <summary>
        /// delegate ResultReturnMethod
        /// </summary>
        /// <param name="kenshumesaiList"></param>
        public delegate void ResultReturnMethod(Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls kenshuIchiranConditionEntity, DataTable dtReturnkenshumeIchiran01, KenshuIshiranConstans.ConditionInfo joken1);
        
        // public delegate void ResultReturnMethod;
        public ResultReturnMethod resultReturnMethod;


        public KenshuIchiranJokenShiteiPopupForm(UIHeader header, int alertNumber, KenshuIshiranConstans.ConditionInfo param, ResultReturnMethod resultMethod = null)
            : base(WINDOW_ID.T_KENSYUU_ICHIRAN_JYOUKEN, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            this.header_new = header;

            // delegateメソッド(戻り値を返す)
            this.resultReturnMethod = resultMethod;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.kenshuIchiranJokenShiteiPopupLogic = new KenshuIchiranJokenShiteiPopupLogic(this,alertNumber);
            
            // 画面表示種別を一旦保存
            this.Joken = param;
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
       // public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            this.kenshuIchiranJokenShiteiPopupLogic.WindowInit(this.Joken);
        }       

        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txt_GyoushaCD_TextChanged(object sender, EventArgs e)
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
        private void txt_GyoushaCDTo_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private void ChangeGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCdFrom = this.txt_GyoushaCD.Text;
            var gyoushaCdTo = this.txt_GyoushaCDTo.Text;

            if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
            {
                // 現場CDテキストボックスの活性状態を初期化
                this.txt_GenbaCD.Enabled = true;
                this.txt_GenbaName.Enabled = true;
                this.customPopupOpenButton4.Enabled = true;
                this.txt_GenbaCDTo.Enabled = true;
                this.txt_GenbaNameTo.Enabled = true;
                this.customPopupOpenButton11.Enabled = true;
            }
            else
            {
                this.txt_GenbaCD.Enabled = false;
                this.txt_GenbaName.Enabled = false;
                this.customPopupOpenButton4.Enabled = false;
                this.txt_GenbaCDTo.Enabled = false;
                this.txt_GenbaNameTo.Enabled = false;
                this.customPopupOpenButton11.Enabled = false;

                this.txt_GenbaCD.Text = String.Empty;
                this.txt_GenbaName.Text = String.Empty;
                this.txt_GenbaCDTo.Text = String.Empty;
                this.txt_GenbaNameTo.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txt_NizumiGyoshaCD_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNizumiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txt_NizumiGyoshaCDTo_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNizumiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private void ChangeNizumiGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCdFrom = this.txt_NizumiGyoshaCD.Text;
            var gyoushaCdTo = this.txt_NizumiGyoshaCDTo.Text;

            if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
            {
                // 現場CDテキストボックスの活性状態を初期化
                this.txt_NizumiGenbaCD.Enabled = true;
                this.txt_NizumiGenbaNameRyaku.Enabled = true;
                this.customPopupOpenButton6.Enabled = true;
                this.txt_NizumiGenbaCDTo.Enabled = true;
                this.txt_NizumiGenbaNameRyakuTo.Enabled = true;
                this.customPopupOpenButton13.Enabled = true;
            }
            else
            {
                this.txt_NizumiGenbaCD.Enabled = false;
                this.txt_NizumiGenbaNameRyaku.Enabled = false;
                this.customPopupOpenButton6.Enabled = false;
                this.txt_NizumiGenbaCDTo.Enabled = false;
                this.txt_NizumiGenbaNameRyakuTo.Enabled = false;
                this.customPopupOpenButton13.Enabled = false;

                this.txt_NizumiGenbaCD.Text = String.Empty;
                this.txt_NizumiGenbaNameRyaku.Text = String.Empty;
                this.txt_NizumiGenbaCDTo.Text = String.Empty;
                this.txt_NizumiGenbaNameRyakuTo.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
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
        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        #region 20151006 hoanghm #12007 検収状況を「1.未検収」にした場合、検収伝票日付範囲選択はブランクかつ編集不可とするよう修正をお願いします。

        private void txt_KenshuJyoukyou_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_KenshuJyoukyou.Text) || this.txt_KenshuJyoukyou.Text.Equals("1"))
            {
                this.dtp_KenshuFrom.Value = null;
                this.dtp_KenshuTo.Value = null; 
                this.dtp_KenshuFrom.Enabled = false;
                this.dtp_KenshuTo.Enabled = false;

                // 未検収に設定している場合は、[検収品名]項目を非活性にする
                this.txt_KenshuHinmeiCD.Enabled = false;
                this.txt_KenshuHinmeiNameRyaku.Enabled = false;
                this.customPopupOpenButton8.Enabled = false;
                this.txt_KenshuHinmeiCDTo.Enabled = false;
                this.txt_KenshuHinmeiNameRyakuTo.Enabled = false;
                this.customPopupOpenButton15.Enabled = false;

                this.txt_KenshuHinmeiCD.Text = String.Empty;
                this.txt_KenshuHinmeiNameRyaku.Text = String.Empty;
                this.txt_KenshuHinmeiCDTo.Text = String.Empty;
                this.txt_KenshuHinmeiNameRyakuTo.Text = String.Empty;

            }
            else
            {
                if (this.dtp_KenshuFrom.Value == null)
                {
                    this.dtp_KenshuFrom.Value = this.sysDate.Date;
                }
                if (this.dtp_KenshuTo.Value == null)
                {
                    this.dtp_KenshuTo.Value = this.sysDate.Date;
                }
                this.dtp_KenshuFrom.Enabled = true;
                this.dtp_KenshuTo.Enabled = true;

                // 検収済に設定している場合は、[検収品名]項目を活性にする
                this.txt_KenshuHinmeiCD.Enabled = true;
                this.txt_KenshuHinmeiNameRyaku.Enabled = true;
                this.customPopupOpenButton8.Enabled = true;
                this.txt_KenshuHinmeiCDTo.Enabled = true;
                this.txt_KenshuHinmeiNameRyakuTo.Enabled = true;
                this.customPopupOpenButton15.Enabled = true;
            }
        }

        #endregion
    }
}
