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
using r_framework.CustomControl;

namespace Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    public partial class ShukeiHyoJokenShiteiPoppupForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        UIHeader header_new; 

       // private r_framework.Logic.IBuisinessLogic logic;
        private ShukeiHyoJokenShiteiPoppupLogic shukeiHyoJokenShiteiPoppupLogic;

        public delegate void ResultReturnMethod(ShukeiHyoJokenDTO conditionEntity, DataTable dtDetail,Dictionary<string,DataTable> dicResult);

        public ResultReturnMethod resultReturnMethod;

        public ShukeiHyoJokenShiteiPoppupForm(UIHeader header, WINDOW_ID window_Id, decimal arlertNumber, ResultReturnMethod resultMethod = null)
         : base(WINDOW_ID.T_DAINO_MEISAI_SYUUKEI_JYOUKEN, WINDOW_TYPE.NONE)           
            
        {
            this.InitializeComponent();           
            this.header_new = header;
            this.resultReturnMethod = resultMethod;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
           this.shukeiHyoJokenShiteiPoppupLogic = new ShukeiHyoJokenShiteiPoppupLogic(window_Id,this, arlertNumber);
            
           // this.shukeiHyoJokenShiteiPoppupLogic = new ShukeiHyoJokenShiteiPoppupLogic(this);

        }
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.shukeiHyoJokenShiteiPoppupLogic.WindowInit();
        }        

        #region 受入現場更新後処理
        /// <summary>
        /// 荷積場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox dbjCd = (CustomTextBox)sender;
                MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();
                //dbjCd.IsInputErrorOccured = false;
                //「範囲条件」に荷積現場を指定する場合は、荷積業者の開始/終了CDを同じにしてください。
                if ((!string.IsNullOrEmpty(this.GYOUSHA_CD_From.Text)
                    || !string.IsNullOrEmpty(this.GYOUSHA_CD_To.Text))
                    && !this.GYOUSHA_CD_From.Text.Equals(this.GYOUSHA_CD_To.Text) && !string.IsNullOrEmpty(dbjCd.Text))
                {
                    myMessageBox.MessageBoxShow("E102", "荷積現場", "荷積業者");
                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    dbjCd.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受入現場更新後処理
        /// <summary>
        /// 荷積場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void GENBA_CD_Export_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox dbjCd = (CustomTextBox)sender;
                MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();
                //dbjCd.IsInputErrorOccured = false;
                //「範囲条件」に荷積現場を指定する場合は、荷積業者の開始/終了CDを同じにしてください。
                if ((!string.IsNullOrEmpty(this.GYOUSHA_CD_Export_From.Text)
                    || !string.IsNullOrEmpty(this.GYOUSHA_CD_Export_To.Text))
                    && !this.GYOUSHA_CD_Export_From.Text.Equals(this.GYOUSHA_CD_Export_To.Text) && !string.IsNullOrEmpty(dbjCd.Text))
                {
                    myMessageBox.MessageBoxShow("E102", "荷積現場", "荷積業者");
                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    dbjCd.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 受入業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_From_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeUkenyuuGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_To_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeUkenyuuGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private void ChangeUkenyuuGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCdFrom = this.GYOUSHA_CD_From.Text;
            var gyoushaCdTo = this.GYOUSHA_CD_To.Text;

            if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
            {
                // 現場CDテキストボックスの活性状態を初期化
                this.GENBA_CD_From.Enabled = true;
                this.GENBA_NAME_RYAKU_From.Enabled = true;
                this.cbtn_GENBA_SEARCH_UKEIRE.Enabled = true;
                this.GENBA_CD_To.Enabled = true;
                this.GENBA_NAME_RYAKU_To.Enabled = true;
                this.cbtn_GENBA_SEARCH_SHUKKA.Enabled = true;
            }
            else
            {
                this.GENBA_CD_From.Enabled = false;
                this.GENBA_NAME_RYAKU_From.Enabled = false;
                this.cbtn_GENBA_SEARCH_UKEIRE.Enabled = false;
                this.GENBA_CD_To.Enabled = false;
                this.GENBA_NAME_RYAKU_To.Enabled = false;
                this.cbtn_GENBA_SEARCH_SHUKKA.Enabled = false;

                this.GENBA_CD_From.Text = String.Empty;
                this.GENBA_NAME_RYAKU_From.Text = String.Empty;
                this.GENBA_CD_To.Text = String.Empty;
                this.GENBA_NAME_RYAKU_To.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Export_From_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeSyukkaGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Export_To_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeSyukkaGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷業者CDの入力状態に応じて現場CDテキストボックスの活性状態を変更します
        /// </summary>
        private void ChangeSyukkaGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCdFrom = this.GYOUSHA_CD_Export_From.Text;
            var gyoushaCdTo = this.GYOUSHA_CD_Export_To.Text;

            if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
            {
                // 現場CDテキストボックスの活性状態を初期化
                this.GENBA_CD_From.Enabled = true;
                this.GENBA_NAME_RYAKU_From.Enabled = true;
                this.cbtn_GENBA_SEARCH_UKEIRE.Enabled = true;
                this.GENBA_CD_To.Enabled = true;
                this.GENBA_NAME_RYAKU_To.Enabled = true;
                this.cbtn_GENBA_SEARCH_SHUKKA.Enabled = true;
            }
            else
            {
                this.GENBA_CD_Export_From.Enabled = false;
                this.GENBA_NAME_RYAKU_Export_From.Enabled = false;
                this.customPopupOpenButton1.Enabled = false;
                this.GENBA_CD_Export_To.Enabled = false;
                this.GENBA_NAME_RYAKU_Export_To.Enabled = false;
                this.customPopupOpenButton4.Enabled = false;

                this.GENBA_CD_Export_From.Text = String.Empty;
                this.GENBA_NAME_RYAKU_Export_From.Text = String.Empty;
                this.GENBA_CD_Export_To.Text = String.Empty;
                this.GENBA_NAME_RYAKU_Export_To.Text = String.Empty;
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
    }
}
